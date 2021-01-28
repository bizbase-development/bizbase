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
using Uniswap.Contracts.UniswapV2Library.ContractDefinition;

namespace Uniswap.Contracts.UniswapV2Library
{
    public partial class UniswapV2LibraryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, UniswapV2LibraryDeployment uniswapV2LibraryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapV2LibraryDeployment>().SendRequestAndWaitForReceiptAsync(uniswapV2LibraryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, UniswapV2LibraryDeployment uniswapV2LibraryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapV2LibraryDeployment>().SendRequestAsync(uniswapV2LibraryDeployment);
        }

        public static async Task<UniswapV2LibraryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, UniswapV2LibraryDeployment uniswapV2LibraryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, uniswapV2LibraryDeployment, cancellationTokenSource);
            return new UniswapV2LibraryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public UniswapV2LibraryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
