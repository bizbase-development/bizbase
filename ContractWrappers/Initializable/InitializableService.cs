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
using ContractWrappers.Contracts.Initializable.ContractDefinition;

namespace ContractWrappers.Contracts.Initializable
{
    public partial class InitializableService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, InitializableDeployment initializableDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<InitializableDeployment>().SendRequestAndWaitForReceiptAsync(initializableDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, InitializableDeployment initializableDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<InitializableDeployment>().SendRequestAsync(initializableDeployment);
        }

        public static async Task<InitializableService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, InitializableDeployment initializableDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, initializableDeployment, cancellationTokenSource);
            return new InitializableService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public InitializableService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
