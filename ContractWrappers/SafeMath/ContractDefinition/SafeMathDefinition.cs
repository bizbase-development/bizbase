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

namespace ContractWrappers.Contracts.SafeMath.ContractDefinition
{


    public partial class SafeMathDeployment : SafeMathDeploymentBase
    {
        public SafeMathDeployment() : base(BYTECODE) { }
        public SafeMathDeployment(string byteCode) : base(byteCode) { }
    }

    public class SafeMathDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566023600b82828239805160001a607314601657fe5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea2646970667358221220617340c89c88bbf38f46e2da93cf1b67fdac7fe80316af27b032a10ea639ffee64736f6c634300060c0033";
        public SafeMathDeploymentBase() : base(BYTECODE) { }
        public SafeMathDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
