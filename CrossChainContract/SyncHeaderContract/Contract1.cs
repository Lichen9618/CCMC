using System;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace SyncHeaderContract
{
    class Contract1 : SmartContract
    {
        public static object Main(string method, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "SyncBlockHeader")
                {
                    return SyncHeader((byte[])args[0]);
                }
                if (method == "GetBlockHeader")
                {
                    return GetHeader((ulong)args[0], (ulong)args[1]);
                }
                if (method == "GetCurrentHeight")
                {
                    return GetCurrentHeight((ulong)args[0]);
                }
                if (method == "GetBlockHeaderHash")
                {
                    return GetBlockHeaderHash((ulong)args[0], (ulong)args[1]);
                }
            }

            return false;
        }

        [Syscall("Neo.CrossChain.SyncBlockHeader")]
        public static extern bool SyncHeader(byte[] paraBytes);

        [Syscall("Neo.CrossChain.GetHeaderByHeight")]
        public static extern byte[] GetHeader(ulong chainID, ulong height);

        [Syscall("Neo.CrossChain.GetCurrentHeight")]
        public static extern ulong GetCurrentHeight(ulong chainID);

        [Syscall("Neo.CrossChain.GetBlockHeaderHash")]
        public static extern byte[] GetBlockHeaderHash(ulong chainID, ulong height);

        [Syscall("Neo.CrossChain.IntToString")]
        public static extern string ToIntString(byte[] paraBytes);
        [Syscall("Neo.CrossChain.ToHexString")]
        public static extern string ToHexString(byte[] paraBytes);
    }
}
