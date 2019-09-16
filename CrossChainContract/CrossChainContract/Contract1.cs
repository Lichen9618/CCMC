using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace CrossChainContract
{
    class Contract1 : SmartContract
    {
        public static bool Main(string method, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "CreateCrossChainTx")
                {
                    return CreateCrossChainTransaction((byte[])args[0]);
                }
            }

            return false;
        }

        [Syscall("Neo.CrossChain.CreateTransaction")]
        public static extern bool CreateCrossChainTransaction(byte[] paraBytes);
    }
}
