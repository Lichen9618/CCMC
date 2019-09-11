using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace SyncHeaderContract
{
    class Contract1 : SmartContract
    {
        public static bool Main(string method,object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "SyncBlockHeader")
                {
                    return SyncHeader((byte[]) args[0]);
                }
            }

            return true;
        }

        [Syscall("Neo.CrossChain.SyncBlockHeader")]
        public static extern bool SyncHeader(byte[] paraBytes);
    }
}
