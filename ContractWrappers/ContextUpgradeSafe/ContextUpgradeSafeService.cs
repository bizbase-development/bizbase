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
using ContractWrappers.Contracts.ContextUpgradeSafe.ContractDefinition;

namespace ContractWrappers.Contracts.ContextUpgradeSafe
{
    public partial class ContextUpgradeSafeService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ContextUpgradeSafeDeployment contextUpgradeSafeDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ContextUpgradeSafeDeployment>().SendRequestAndWaitForReceiptAsync(contextUpgradeSafeDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ContextUpgradeSafeDeployment contextUpgradeSafeDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ContextUpgradeSafeDeployment>().SendRequestAsync(contextUpgradeSafeDeployment);
        }

        public static async Task<ContextUpgradeSafeService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ContextUpgradeSafeDeployment contextUpgradeSafeDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, contextUpgradeSafeDeployment, cancellationTokenSource);
            return new ContextUpgradeSafeService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ContextUpgradeSafeService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
