using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace SyncHeaderContract
{
    class Contract1 : SmartContract
    {
        public static object Main(string method,object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "SyncBlockHeader")
                {
                    return SyncHeader((byte[]) args[0]);
                }

                if (method == "GetCurrentHeight")
                {
                    //获取本合约地址hash
                    var contract = ExecutionEngine.ExecutingScriptHash;
                    var chainIDBytes = (byte[])args[0];
                    Runtime.Log("chain id is:"+ ToIntString(chainIDBytes));
                    var key = contract.Concat("CURRENT_HEIGHT".AsByteArray()).Concat(chainIDBytes);
                    Runtime.Log("chain height key is:"+ToHexString(key));
                    return Storage.Get(key).AsBigInteger();
                }
                if (method == "GetBlockHeaderHash")
                {
                    //获取本合约地址hash
                    var contract = ExecutionEngine.ExecutingScriptHash;
                    var chainIDBytes = (byte[])args[0];
                    var heightBytes = (byte[]) args[1];
                    var key = "HEADER_INDEX".AsByteArray().Concat(chainIDBytes).Concat(heightBytes);
                    return Storage.Get(key);
                }
            }

            return false;
        }

        [Syscall("Neo.CrossChain.SyncBlockHeader")]
        public static extern bool SyncHeader(byte[] paraBytes);

        [Syscall("Neo.CrossChain.IntToString")]
        public static extern string ToIntString(byte[] paraBytes);
        [Syscall("Neo.CrossChain.ToHexString")]
        public static extern string ToHexString(byte[] paraBytes);
    }
}
