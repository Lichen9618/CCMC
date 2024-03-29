﻿using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace CrossChainContract
{
    class Contract : SmartContract
    {
        delegate object DynCall(string method, object[] args);
        public static object Main(string method, object[] args)
        {

            if (method == "CreateCrossChainTx")
            {
                return CreateCrossChainTransaction((long)args[0], (byte[])args[1], (string)args[2], (byte[])args[3]);
            }

            if (method == "GetCrossChainRequestID")
            {
                return Storage.Get("REQUEST_ID".AsByteArray().Concat((byte[]) args[0])).AsBigInteger();
            }

            if (method == "GetCrossChainRequest")
            {
                return Storage.Get("REQUEST".AsByteArray().Concat((byte[])args[0]).Concat((byte[])args[1])).AsBigInteger();
            }


            if (method == "ProcessCrossChainTx")
            {
                object[] parameters = DeserializeParameters((byte[])args[0], (byte[])args[1]);
                byte[] Target = (byte[])parameters[0];
                string operation = (string)parameters[1];
                byte[] Args = (byte[])parameters[2];
                DynCall TargetContract = (DynCall)Target.ToDelegate();
                object[] parameter = new object[]
                {
                    Args
                };
                return TargetContract(operation, parameter);
            }
            return true;
        }
        [Syscall("Neo.CrossChain.CreateTransaction")]
        public static extern bool CreateCrossChainTransaction(long chainID, byte[] contractAddress, string functionName, byte[] paraBytes);
        [Syscall("Neo.CrossChain.DeserializeParameters")]
        public static extern object[] DeserializeParameters(byte[] path, byte[] root);
        [Syscall("Neo.CrossChain.ToHexString")]
        public static extern string ToHexString(byte[] paraBytes);

    }
}
