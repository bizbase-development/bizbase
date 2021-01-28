using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using ContractWrappers.Contracts.Bizbase.ContractDefinition;

namespace ContractWrappers.Contracts.Bizbase
{
    public partial class BizbaseService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BizbaseDeployment bizbaseDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BizbaseDeployment>().SendRequestAndWaitForReceiptAsync(bizbaseDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BizbaseDeployment bizbaseDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BizbaseDeployment>().SendRequestAsync(bizbaseDeployment);
        }

        public static async Task<BizbaseService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BizbaseDeployment bizbaseDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, bizbaseDeployment, cancellationTokenSource);
            return new BizbaseService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public BizbaseService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> RebaserQueryAsync(RebaserFunction rebaserFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RebaserFunction, string>(rebaserFunction, blockParameter);
        }

        
        public Task<string> RebaserQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RebaserFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TFeePercentQueryAsync(TFeePercentFunction tFeePercentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TFeePercentFunction, BigInteger>(tFeePercentFunction, blockParameter);
        }

        
        public Task<BigInteger> TFeePercentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TFeePercentFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> AddTransactionRequestAsync(AddTransactionFunction addTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(addTransactionFunction);
        }

        public Task<TransactionReceipt> AddTransactionRequestAndWaitForReceiptAsync(AddTransactionFunction addTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addTransactionFunction, cancellationToken);
        }

        public Task<string> AddTransactionRequestAsync(string destination, byte[] data)
        {
            var addTransactionFunction = new AddTransactionFunction();
                addTransactionFunction.Destination = destination;
                addTransactionFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(addTransactionFunction);
        }

        public Task<TransactionReceipt> AddTransactionRequestAndWaitForReceiptAsync(string destination, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var addTransactionFunction = new AddTransactionFunction();
                addTransactionFunction.Destination = destination;
                addTransactionFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addTransactionFunction, cancellationToken);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        
        public Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null)
        {
            var allowanceFunction = new AllowanceFunction();
                allowanceFunction.Owner = owner;
                allowanceFunction.Spender = spender;
            
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        public Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
        {
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<string> ApproveRequestAsync(string spender, BigInteger amount)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        
        public Task<BigInteger> BalanceOfQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var balanceOfFunction = new BalanceOfFunction();
                balanceOfFunction.Account = account;
            
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
        }

        
        public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
        }

        public Task<string> DecreaseAllowanceRequestAsync(DecreaseAllowanceFunction decreaseAllowanceFunction)
        {
             return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
        }

        public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(DecreaseAllowanceFunction decreaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
        }

        public Task<string> DecreaseAllowanceRequestAsync(string spender, BigInteger subtractedValue)
        {
            var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
                decreaseAllowanceFunction.Spender = spender;
                decreaseAllowanceFunction.SubtractedValue = subtractedValue;
            
             return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
        }

        public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger subtractedValue, CancellationTokenSource cancellationToken = null)
        {
            var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
                decreaseAllowanceFunction.Spender = spender;
                decreaseAllowanceFunction.SubtractedValue = subtractedValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
        }

        public Task<string> ExcludeAccountRequestAsync(ExcludeAccountFunction excludeAccountFunction)
        {
             return ContractHandler.SendRequestAsync(excludeAccountFunction);
        }

        public Task<TransactionReceipt> ExcludeAccountRequestAndWaitForReceiptAsync(ExcludeAccountFunction excludeAccountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(excludeAccountFunction, cancellationToken);
        }

        public Task<string> ExcludeAccountRequestAsync(string account)
        {
            var excludeAccountFunction = new ExcludeAccountFunction();
                excludeAccountFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(excludeAccountFunction);
        }

        public Task<TransactionReceipt> ExcludeAccountRequestAndWaitForReceiptAsync(string account, CancellationTokenSource cancellationToken = null)
        {
            var excludeAccountFunction = new ExcludeAccountFunction();
                excludeAccountFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(excludeAccountFunction, cancellationToken);
        }

        public Task<string> IncludeAccountRequestAsync(IncludeAccountFunction includeAccountFunction)
        {
             return ContractHandler.SendRequestAsync(includeAccountFunction);
        }

        public Task<TransactionReceipt> IncludeAccountRequestAndWaitForReceiptAsync(IncludeAccountFunction includeAccountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(includeAccountFunction, cancellationToken);
        }

        public Task<string> IncludeAccountRequestAsync(string account)
        {
            var includeAccountFunction = new IncludeAccountFunction();
                includeAccountFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(includeAccountFunction);
        }

        public Task<TransactionReceipt> IncludeAccountRequestAndWaitForReceiptAsync(string account, CancellationTokenSource cancellationToken = null)
        {
            var includeAccountFunction = new IncludeAccountFunction();
                includeAccountFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(includeAccountFunction, cancellationToken);
        }

        public Task<string> IncreaseAllowanceRequestAsync(IncreaseAllowanceFunction increaseAllowanceFunction)
        {
             return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
        }

        public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(IncreaseAllowanceFunction increaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
        }

        public Task<string> IncreaseAllowanceRequestAsync(string spender, BigInteger addedValue)
        {
            var increaseAllowanceFunction = new IncreaseAllowanceFunction();
                increaseAllowanceFunction.Spender = spender;
                increaseAllowanceFunction.AddedValue = addedValue;
            
             return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
        }

        public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger addedValue, CancellationTokenSource cancellationToken = null)
        {
            var increaseAllowanceFunction = new IncreaseAllowanceFunction();
                increaseAllowanceFunction.Spender = spender;
                increaseAllowanceFunction.AddedValue = addedValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
        }

        public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
        {
             return ContractHandler.SendRequestAsync(initializeFunction);
        }

        public Task<string> InitializeRequestAsync()
        {
             return ContractHandler.SendRequestAsync<InitializeFunction>();
        }

        public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
        }

        public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<InitializeFunction>(null, cancellationToken);
        }

        public Task<bool> IsExcludedQueryAsync(IsExcludedFunction isExcludedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsExcludedFunction, bool>(isExcludedFunction, blockParameter);
        }

        
        public Task<bool> IsExcludedQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var isExcludedFunction = new IsExcludedFunction();
                isExcludedFunction.Account = account;
            
            return ContractHandler.QueryAsync<IsExcludedFunction, bool>(isExcludedFunction, blockParameter);
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
        }

        
        public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> RebaseRequestAsync(RebaseFunction rebaseFunction)
        {
             return ContractHandler.SendRequestAsync(rebaseFunction);
        }

        public Task<TransactionReceipt> RebaseRequestAndWaitForReceiptAsync(RebaseFunction rebaseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rebaseFunction, cancellationToken);
        }

        public Task<string> RebaseRequestAsync(BigInteger supplyDelta)
        {
            var rebaseFunction = new RebaseFunction();
                rebaseFunction.SupplyDelta = supplyDelta;
            
             return ContractHandler.SendRequestAsync(rebaseFunction);
        }

        public Task<TransactionReceipt> RebaseRequestAndWaitForReceiptAsync(BigInteger supplyDelta, CancellationTokenSource cancellationToken = null)
        {
            var rebaseFunction = new RebaseFunction();
                rebaseFunction.SupplyDelta = supplyDelta;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rebaseFunction, cancellationToken);
        }

        public Task<string> RefractRequestAsync(RefractFunction refractFunction)
        {
             return ContractHandler.SendRequestAsync(refractFunction);
        }

        public Task<TransactionReceipt> RefractRequestAndWaitForReceiptAsync(RefractFunction refractFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(refractFunction, cancellationToken);
        }

        public Task<string> RefractRequestAsync(BigInteger tAmount)
        {
            var refractFunction = new RefractFunction();
                refractFunction.TAmount = tAmount;
            
             return ContractHandler.SendRequestAsync(refractFunction);
        }

        public Task<TransactionReceipt> RefractRequestAndWaitForReceiptAsync(BigInteger tAmount, CancellationTokenSource cancellationToken = null)
        {
            var refractFunction = new RefractFunction();
                refractFunction.TAmount = tAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(refractFunction, cancellationToken);
        }

        public Task<BigInteger> RefractionFromTokenQueryAsync(RefractionFromTokenFunction refractionFromTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RefractionFromTokenFunction, BigInteger>(refractionFromTokenFunction, blockParameter);
        }

        
        public Task<BigInteger> RefractionFromTokenQueryAsync(BigInteger tAmount, bool deductTransferFee, BlockParameter blockParameter = null)
        {
            var refractionFromTokenFunction = new RefractionFromTokenFunction();
                refractionFromTokenFunction.TAmount = tAmount;
                refractionFromTokenFunction.DeductTransferFee = deductTransferFee;
            
            return ContractHandler.QueryAsync<RefractionFromTokenFunction, BigInteger>(refractionFromTokenFunction, blockParameter);
        }

        public Task<string> RemoveTransactionRequestAsync(RemoveTransactionFunction removeTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(removeTransactionFunction);
        }

        public Task<TransactionReceipt> RemoveTransactionRequestAndWaitForReceiptAsync(RemoveTransactionFunction removeTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeTransactionFunction, cancellationToken);
        }

        public Task<string> RemoveTransactionRequestAsync(BigInteger index)
        {
            var removeTransactionFunction = new RemoveTransactionFunction();
                removeTransactionFunction.Index = index;
            
             return ContractHandler.SendRequestAsync(removeTransactionFunction);
        }

        public Task<TransactionReceipt> RemoveTransactionRequestAndWaitForReceiptAsync(BigInteger index, CancellationTokenSource cancellationToken = null)
        {
            var removeTransactionFunction = new RemoveTransactionFunction();
                removeTransactionFunction.Index = index;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeTransactionFunction, cancellationToken);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> SetRebaserRequestAsync(SetRebaserFunction setRebaserFunction)
        {
             return ContractHandler.SendRequestAsync(setRebaserFunction);
        }

        public Task<TransactionReceipt> SetRebaserRequestAndWaitForReceiptAsync(SetRebaserFunction setRebaserFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRebaserFunction, cancellationToken);
        }

        public Task<string> SetRebaserRequestAsync(string rebaser)
        {
            var setRebaserFunction = new SetRebaserFunction();
                setRebaserFunction.Rebaser = rebaser;
            
             return ContractHandler.SendRequestAsync(setRebaserFunction);
        }

        public Task<TransactionReceipt> SetRebaserRequestAndWaitForReceiptAsync(string rebaser, CancellationTokenSource cancellationToken = null)
        {
            var setRebaserFunction = new SetRebaserFunction();
                setRebaserFunction.Rebaser = rebaser;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRebaserFunction, cancellationToken);
        }

        public Task<string> SetTransactionEnabledRequestAsync(SetTransactionEnabledFunction setTransactionEnabledFunction)
        {
             return ContractHandler.SendRequestAsync(setTransactionEnabledFunction);
        }

        public Task<TransactionReceipt> SetTransactionEnabledRequestAndWaitForReceiptAsync(SetTransactionEnabledFunction setTransactionEnabledFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransactionEnabledFunction, cancellationToken);
        }

        public Task<string> SetTransactionEnabledRequestAsync(BigInteger index, bool enabled)
        {
            var setTransactionEnabledFunction = new SetTransactionEnabledFunction();
                setTransactionEnabledFunction.Index = index;
                setTransactionEnabledFunction.Enabled = enabled;
            
             return ContractHandler.SendRequestAsync(setTransactionEnabledFunction);
        }

        public Task<TransactionReceipt> SetTransactionEnabledRequestAndWaitForReceiptAsync(BigInteger index, bool enabled, CancellationTokenSource cancellationToken = null)
        {
            var setTransactionEnabledFunction = new SetTransactionEnabledFunction();
                setTransactionEnabledFunction.Index = index;
                setTransactionEnabledFunction.Enabled = enabled;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransactionEnabledFunction, cancellationToken);
        }

        public Task<string> SetTransferFeePercentRequestAsync(SetTransferFeePercentFunction setTransferFeePercentFunction)
        {
             return ContractHandler.SendRequestAsync(setTransferFeePercentFunction);
        }

        public Task<TransactionReceipt> SetTransferFeePercentRequestAndWaitForReceiptAsync(SetTransferFeePercentFunction setTransferFeePercentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransferFeePercentFunction, cancellationToken);
        }

        public Task<string> SetTransferFeePercentRequestAsync(BigInteger tFeePercent)
        {
            var setTransferFeePercentFunction = new SetTransferFeePercentFunction();
                setTransferFeePercentFunction.TFeePercent = tFeePercent;
            
             return ContractHandler.SendRequestAsync(setTransferFeePercentFunction);
        }

        public Task<TransactionReceipt> SetTransferFeePercentRequestAndWaitForReceiptAsync(BigInteger tFeePercent, CancellationTokenSource cancellationToken = null)
        {
            var setTransferFeePercentFunction = new SetTransferFeePercentFunction();
                setTransferFeePercentFunction.TFeePercent = tFeePercent;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransferFeePercentFunction, cancellationToken);
        }

        public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
        }

        
        public Task<string> SymbolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TokenFromRefractionQueryAsync(TokenFromRefractionFunction tokenFromRefractionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFromRefractionFunction, BigInteger>(tokenFromRefractionFunction, blockParameter);
        }

        
        public Task<BigInteger> TokenFromRefractionQueryAsync(BigInteger rAmount, BlockParameter blockParameter = null)
        {
            var tokenFromRefractionFunction = new TokenFromRefractionFunction();
                tokenFromRefractionFunction.RAmount = rAmount;
            
            return ContractHandler.QueryAsync<TokenFromRefractionFunction, BigInteger>(tokenFromRefractionFunction, blockParameter);
        }

        public Task<BigInteger> TotalFeesQueryAsync(TotalFeesFunction totalFeesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalFeesFunction, BigInteger>(totalFeesFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalFeesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalFeesFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(TransactionsFunction transactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var transactionsFunction = new TransactionsFunction();
                transactionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<BigInteger> TransactionsSizeQueryAsync(TransactionsSizeFunction transactionsSizeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TransactionsSizeFunction, BigInteger>(transactionsSizeFunction, blockParameter);
        }

        
        public Task<BigInteger> TransactionsSizeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TransactionsSizeFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TransferRequestAsync(TransferFunction transferFunction)
        {
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferRequestAsync(string recipient, BigInteger amount)
        {
            var transferFunction = new TransferFunction();
                transferFunction.Recipient = recipient;
                transferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string recipient, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var transferFunction = new TransferFunction();
                transferFunction.Recipient = recipient;
                transferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
        {
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(string sender, string recipient, BigInteger amount)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.Sender = sender;
                transferFromFunction.Recipient = recipient;
                transferFromFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string sender, string recipient, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.Sender = sender;
                transferFromFunction.Recipient = recipient;
                transferFromFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
    }
}
