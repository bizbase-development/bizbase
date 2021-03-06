using bizbase;
using ContractWrappers.Contracts.Bizbase;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uniswap.Contracts.UniswapV2Router02;



if (!Directory.Exists("files")) Directory.CreateDirectory("files");
if (!Directory.Exists("webserver")) Directory.CreateDirectory("webserver");


var configFile = $"files{Path.DirectorySeparatorChar}config.json";
//File.WriteAllText(configFile, JsonConvert.SerializeObject(new Config(), Formatting.Indented));
var cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));

var errorFile = $"files{Path.DirectorySeparatorChar}errlog.txt";
var bzbFile = $"files{Path.DirectorySeparatorChar}bzbdat.json";

var bzbFile_old = $"files{Path.DirectorySeparatorChar}bzbdat_old.json";


if (!File.Exists(bzbFile) || File.ReadAllText(bzbFile).Length < 1)
	File.WriteAllText(bzbFile, JsonConvert.SerializeObject(new List<IntervalData>(), Formatting.Indented));

List<IntervalData> intervals_old = new List<IntervalData>();
if(File.Exists(bzbFile_old))
	intervals_old = JsonConvert.DeserializeObject<List<IntervalData>>(File.ReadAllText(bzbFile_old));
var intervals = JsonConvert.DeserializeObject<List<IntervalData>>(File.ReadAllText(bzbFile));

async Task updatePostCount() {
	List<Root> catalog;
	using (var hc = new HttpClient()) {
		var response = await hc.GetAsync("https://a.4cdn.org/biz/catalog.json");
		var str = await response.Content.ReadAsStringAsync();
		catalog = JsonConvert.DeserializeObject<List<Root>>(str);
	}
	var currentLastPostNumber = catalog
		.SelectMany(ct => ct.threads)
		.Where(th => th.last_replies != null)
		.SelectMany(th => th.last_replies)
		.Max(r => r.no);
		//.First(th => th.closed != 1 && th.replies != 0).last_replies.Last().no;
	var currentPostCount = -1;

	IntervalData lastInterval = null;
	if (intervals.Count != 0)
		lastInterval = intervals.Last();
	else if (intervals_old.Count != 0)
		lastInterval = intervals_old.Last();

	if (lastInterval != null && DateTime.UtcNow - lastInterval.Time < TimeSpan.FromHours(36))
		if (!cfg.Debug_MinuteRun || (DateTime.UtcNow - lastInterval.Time) < TimeSpan.FromSeconds(90))
			currentPostCount = currentLastPostNumber - lastInterval.PostNumber;
	intervals.Add(new IntervalData() {
		Time = DateTime.Now, PostNumber = currentLastPostNumber, PostCount = currentPostCount
	});

	//File.WriteAllText(bzbFile, JsonConvert.SerializeObject(intervals, Formatting.Indented));
}

async Task refreshJsonVolumeData() {
	async Task uploadFile(string fileName, string content) {
		FtpWebRequest request = (FtpWebRequest)WebRequest.Create(cfg.rootPath + fileName);
		request.Method = WebRequestMethods.Ftp.UploadFile;
		request.Credentials = new NetworkCredential(cfg.hostUsername, cfg.hostPassword);
		request.ContentLength = content.Length;
		using (var stream = await request.GetRequestStreamAsync()) {
			var bytes = Encoding.UTF8.GetBytes(content);
			await stream.WriteAsync(bytes, 0, bytes.Length);
		}
	}

	var intervalsWihOldData = intervals_old.Concat(intervals);

	var labels_OldDat = intervalsWihOldData.Skip(1).Select(iv => iv.Time.ToString("MMM-d"));
	var data = intervalsWihOldData.Skip(1).Select(iv => iv.PostCount);
	var json = new { labels = labels_OldDat, data = data };
	//File.WriteAllText($"webserver{Path.DirectorySeparatorChar}volumedata.json", JsonConvert.SerializeObject(json));
	await uploadFile("volumedata.json", JsonConvert.SerializeObject(json));

	var labels_CurrentDat = intervals.Select(intv => intv.Time.ToString("MMM-d"));
	var tsData = intervals.Select(intv => intv.TotalSupplyAfter);
	var json_TS = new { labels = labels_CurrentDat, data = tsData };
	//File.WriteAllText($"webserver{Path.DirectorySeparatorChar}supply.json", JsonConvert.SerializeObject(json_TS));
	await uploadFile("supply.json", JsonConvert.SerializeObject(json_TS));

	var data_marketCap = intervals.Select(intv => intv.TotalSupplyAfter * intv.PostPrice);
	var json_marketCap = new { labels = labels_CurrentDat, data = data_marketCap };
	//File.WriteAllText($"webserver{Path.DirectorySeparatorChar}marketcap.json", JsonConvert.SerializeObject(json_marketCap));
	await uploadFile("marketcap.json", JsonConvert.SerializeObject(json_marketCap));

	var data_marketCapETH = intervals.Select(intv => intv.TotalSupplyAfter * intv.PostPriceETH);
	var json_marketCapETH = new { labels = labels_CurrentDat, data = data_marketCapETH };
	//File.WriteAllText($"webserver{Path.DirectorySeparatorChar}marketcapETH.json", JsonConvert.SerializeObject(json_marketCapETH));
	await uploadFile("marketcapETH.json", JsonConvert.SerializeObject(json_marketCapETH));
}

if(!cfg.Debug_MinuteRun)
	await refreshJsonVolumeData();
while (true) {
	try {
		//await updateContract(50);
		DateTime targetTime;
		if (cfg.Debug_MinuteRun) {
			targetTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, cfg.RebaseHourUTC, 0);
			if (DateTime.UtcNow.Second >= cfg.RebaseHourUTC) targetTime = targetTime.AddMinutes(1);
		}
		else {
			targetTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, cfg.RebaseHourUTC, 0, 0, 0);
			if (DateTime.UtcNow.Hour >= cfg.RebaseHourUTC) targetTime = targetTime.AddDays(1);
		}

		var timeToWait = targetTime - DateTime.UtcNow;
		Console.WriteLine($"{DateTime.UtcNow.ToString("o")} waiting: {timeToWait}");
		await Task.Delay((int)timeToWait.TotalMilliseconds);
		Console.WriteLine($"{DateTime.UtcNow.ToString("o")} updating postcount");
		await updatePostCount();
		if (intervals.Last().PostCount > 0) {
			Console.WriteLine($"{DateTime.UtcNow.ToString("o")} updating contract");
			var dat = await updateContract(intervals.Last().PostCount);
			
			intervals[intervals.Count - 1].PrePrice = dat.prePrice;
			intervals[intervals.Count - 1].PostPrice = dat.postPrice;
			intervals[intervals.Count - 1].PrePriceETH = dat.prePriceETH;
			intervals[intervals.Count - 1].PostPriceETH = dat.postPriceETH;
			intervals[intervals.Count - 1].TotalSupplyBefore = dat.totalSupplyBefore;
			intervals[intervals.Count - 1].TotalSupplyAfter = dat.totalSupplyAfter;
			intervals[intervals.Count - 1].SupplyDelta = dat.supplyDelta;

			if (intervals.Last().PrePrice == 0m) { }
			await refreshJsonVolumeData();
		}

		File.WriteAllText(bzbFile, JsonConvert.SerializeObject(intervals, Formatting.Indented));
	} catch (Exception e) {
		File.AppendAllText(errorFile, Environment.NewLine + DateTime.UtcNow.ToLongTimeString() + " " + e.ToString() + Environment.NewLine);
	}
}


async Task<(decimal prePrice, decimal postPrice, decimal prePriceETH, decimal postPriceETH, decimal totalSupplyBefore, decimal totalSupplyAfter, decimal supplyDelta)> updateContract(int postCount) {
	var addresses = ETHAddressCollection.ByNetwork(cfg.Network);

	using var hc = new HttpClient();
	var getGasPriceResonseAsync = hc.GetAsync(
		"https://data-api.defipulse.com/api/v1/egs/api/ethgasAPI.json?api-key=ed13e4b17ccab4cca1918a5389276534c6c8095ad9fd31923c33dd64be1a"
	);
	////https://data-api.defipulse.com/api/v1/egs/api/ethgasAPI.json?api-key=ed13e4b17ccab4cca1918a5389276534c6c8095ad9fd31923c33dd64be1a
	//var getGasPrices 

	var account = new Account(new string(cfg.PrivateKey.Reverse().ToArray()));
	var web3 = new Web3(account, cfg.InfuraURL);
	
	var serv = new BizbaseService(web3, cfg.ContractAddress);
	//web3.TransactionManager.DefaultGasPrice
	var totalSupply = await serv.TotalSupplyQueryAsync().ToDec9d();
	var router = new UniswapV2Router02Service(web3, addresses.UniswapRouter);


	var wethDai_path = new List<string>() {
		cfg.ContractAddress, addresses.WETH, addresses.DAI
	};

	var _amountsOut = await router.GetAmountsOutQueryAsync(
		1m.ToBigInt9d(), wethDai_path
	);
	var priceETH = _amountsOut[1].ToDec();
	var price = _amountsOut.Last().ToDec();

	var supplyDelta = CalculateSupplyDelta(
		totalySupply: totalSupply,
		price: price,
		oneDollarTargetPostCount: cfg.OneDollarTargetPostCount,
		postCount: postCount
	);
	supplyDelta /= cfg.DampeningFactor;

	try {
		var gasPriceResponse = await getGasPriceResonseAsync;
		var gasPriceJToken = JsonConvert.DeserializeObject<JToken>(await gasPriceResponse.Content.ReadAsStringAsync());
		var gasprice = int.Parse(gasPriceJToken[cfg.gasPricePriority].ToString()) / 10;
		if (gasprice > cfg.maxGasPrice) gasprice = int.Parse(gasPriceJToken["safeLow"].ToString()) / 10;
		if (gasprice > cfg.maxGasPrice) gasprice = cfg.maxGasPrice;
		web3.Eth.TransactionManager.DefaultGasPrice = (UInt64)gasprice * 1_000_000_000ul;
	} catch {
		web3.Eth.TransactionManager.DefaultGasPrice = -1;
	}
	

	await serv.RebaseRequestAndWaitForReceiptAsync(supplyDelta.ToBigInt9d());

	var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
	while ((await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync()) == blockNumber) await Task.Delay(1000);
	await Task.Delay(2000);

	_amountsOut = await router.GetAmountsOutQueryAsync(
		1m.ToBigInt9d(), wethDai_path
	);
	var postPrice = _amountsOut.Last().ToDec();
	var postPriceETH = _amountsOut[1].ToDec();
	var totalSupplyPost = await serv.TotalSupplyQueryAsync().ToDec9d();
	return (price, postPrice, priceETH, postPriceETH, totalSupply, totalSupplyPost, supplyDelta);
}

decimal CalculateSupplyDelta(decimal totalySupply, decimal price, decimal oneDollarTargetPostCount, decimal postCount) {
	//var sd = totalySupply * (price - (postCount / oneDollarTargetPostCount)) / (postCount / oneDollarTargetPostCount);
	var sd = totalySupply * ((price * oneDollarTargetPostCount) - postCount) / postCount;
	return sd;
}


public class IntervalData {
	public DateTime Time; 
	public int PostCount; 
	public int PostNumber;

	public decimal PrePrice;
	public decimal PostPrice;
	public decimal TotalSupplyBefore;
	public decimal TotalSupplyAfter;
	public decimal SupplyDelta;

	public decimal PrePriceETH;
	public decimal PostPriceETH;
}
public class BizBaseData {
	public List<IntervalData> Intervals = new List<IntervalData>();
}

public class Config {
	public string ContractAddress = "";
	public string PrivateKey = "";
	public string InfuraURL = "";
	public int OneDollarTargetPostCount = 0;
	public string NetworkName = "";
	[JsonIgnore]
	public ETHNetwork Network => Enum.Parse<ETHNetwork>(NetworkName);
	public bool Debug_MinuteRun = false;
	public int DampeningFactor = 10;
	public int RebaseHourUTC = 18;
	public string gasPricePriority = "average";
	public int maxGasPrice = 300;

	public string rootPath;
	public string hostUsername;
	public string hostPassword;
}


