using Nethereum.ABI;
using Nethereum.Web3;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Uniswap.Contracts.IUniswapV2Factory;
using Uniswap.Contracts.UniswapV2Router02;

using static bizbase.Int256;

namespace bizbase {
	public enum ETHNetwork { Mainnet, Ropsten }

	public class ETHAddressCollection {
		public string UniswapFactory;
		public string UniswapRouter;
		public string WETH;
		public string DAI;

		public ETHAddressCollection(ETHNetwork nw) {
			UniswapFactory = "0x5C69bEe701ef814a2B6a3EDD4B1652CB9cc5aA6f";
			UniswapRouter = "0x7a250d5630b4cf539739df2c5dacb4c659f2488d";
			switch (nw) {
				case ETHNetwork.Ropsten:
					DAI = "0xad6d458402f60fd3bd25163575031acdce07538d";
					WETH = "0xc778417e063141139fce010982780140aa0cd5ab";
					break;
				case ETHNetwork.Mainnet:
					DAI = "0x6b175474e89094c44da98b954eedeac495271d0f";
					WETH = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2";
					break;
				default: throw new NotImplementedException();
			}
		}

		public static ETHAddressCollection ByNetwork(ETHNetwork nw) => new ETHAddressCollection(nw);
		public static ETHAddressCollection Ropsten => ByNetwork(ETHNetwork.Ropsten);
	}

	public static class Int256 {
		public static int NumberOfDecimals = 9;


		public static Task<decimal> ToDec9d(this Task<BigInteger> t) => t.ContinueWith(_t => _t.Result.ToDec9d());


		public static decimal ToDec9d(this BigInteger n) => (decimal)(n / 100000) / 10000ul;
		public static decimal ToDec(this BigInteger n) => (decimal)(n / 100000) / 10000000000000ul;

		public static BigInteger ToBigInt9d(this decimal d) => Multiply(d, new BigInteger(1000000000ul));
		public static BigInteger ToBigInt(this decimal d) => Multiply(d, new BigInteger(1000000000000000000ul));

		static BigInteger Multiply(decimal d, BigInteger n) {
			var f = Fraction(d);
			return n * f.numerator / f.denominator;
		}
		static (BigInteger numerator, BigInteger denominator) Fraction(decimal d) {
			int[] bits = decimal.GetBits(d);
			BigInteger numerator = (1 - ((bits[3] >> 30) & 2)) *
														 unchecked(((BigInteger)(uint)bits[2] << 64) |
																			 ((BigInteger)(uint)bits[1] << 32) |
																				(BigInteger)(uint)bits[0]);
			BigInteger denominator = BigInteger.Pow(10, (bits[3] >> 16) & 0xff);
			return (numerator, denominator);
		}

	}
}
