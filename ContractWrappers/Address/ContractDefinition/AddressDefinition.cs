using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace ContractWrappers.Contracts.Address.ContractDefinition
{


    public partial class AddressDeployment : AddressDeploymentBase
    {
        public AddressDeployment() : base(BYTECODE) { }
        public AddressDeployment(string byteCode) : base(byteCode) { }
    }

    public class AddressDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566023600b82828239805160001a607314601657fe5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea264697066735822122047c02d5040e77853e72d0f56f0c2cabaa5c758941051b9884f8f012a2cc2451164736f6c634300060c0033";
        public AddressDeploymentBase() : base(BYTECODE) { }
        public AddressDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
