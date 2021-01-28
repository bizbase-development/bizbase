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

namespace Uniswap.Contracts.UniswapV2Library.ContractDefinition
{


    public partial class UniswapV2LibraryDeployment : UniswapV2LibraryDeploymentBase
    {
        public UniswapV2LibraryDeployment() : base(BYTECODE) { }
        public UniswapV2LibraryDeployment(string byteCode) : base(byteCode) { }
    }

    public class UniswapV2LibraryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566023600b82828239805160001a607314601657fe5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea2646970667358221220f9de33f4addd7f29a5a2b62cffd2fbebc52bc88da37997491793c78a593355b064736f6c63430006060033";
        public UniswapV2LibraryDeploymentBase() : base(BYTECODE) { }
        public UniswapV2LibraryDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
