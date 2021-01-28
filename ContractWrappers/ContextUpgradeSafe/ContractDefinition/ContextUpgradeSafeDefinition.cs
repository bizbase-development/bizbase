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

namespace ContractWrappers.Contracts.ContextUpgradeSafe.ContractDefinition
{


    public partial class ContextUpgradeSafeDeployment : ContextUpgradeSafeDeploymentBase
    {
        public ContextUpgradeSafeDeployment() : base(BYTECODE) { }
        public ContextUpgradeSafeDeployment(string byteCode) : base(byteCode) { }
    }

    public class ContextUpgradeSafeDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea264697066735822122052813a384681d62df5390b5a927e71e055d695b3106a348cb1f45ada20c16bef64736f6c634300060c0033";
        public ContextUpgradeSafeDeploymentBase() : base(BYTECODE) { }
        public ContextUpgradeSafeDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
