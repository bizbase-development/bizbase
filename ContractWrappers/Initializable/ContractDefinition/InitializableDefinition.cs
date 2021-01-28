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

namespace ContractWrappers.Contracts.Initializable.ContractDefinition
{


    public partial class InitializableDeployment : InitializableDeploymentBase
    {
        public InitializableDeployment() : base(BYTECODE) { }
        public InitializableDeployment(string byteCode) : base(byteCode) { }
    }

    public class InitializableDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea264697066735822122071738f544e4815a40f3c43c933a399d7adef3c4019929784499842838fd82f6264736f6c634300060c0033";
        public InitializableDeploymentBase() : base(BYTECODE) { }
        public InitializableDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
