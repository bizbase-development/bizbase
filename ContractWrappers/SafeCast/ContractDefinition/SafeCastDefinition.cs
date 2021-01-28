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

namespace ContractWrappers.Contracts.SafeCast.ContractDefinition
{


    public partial class SafeCastDeployment : SafeCastDeploymentBase
    {
        public SafeCastDeployment() : base(BYTECODE) { }
        public SafeCastDeployment(string byteCode) : base(byteCode) { }
    }

    public class SafeCastDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566023600b82828239805160001a607314601657fe5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea2646970667358221220d6a9ed5753889c211aa5404ccdc79d2c07afd8f9b34d2573daefd9255ceb877664736f6c634300060c0033";
        public SafeCastDeploymentBase() : base(BYTECODE) { }
        public SafeCastDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
