using System;
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
                return CreateCrossChainTransaction((byte[])args[0]);
            }

            if (method == "processCrossChainTx")
            {
                byte[] Target = (byte[])args[0];
                string operation = "ProcessCrossChainTransfer";
                object[] parameters = new object[args.Length - 1];
                args.CopyTo(parameters, 2);
                DynCall TargetContract = (DynCall)Target.ToDelegate();
                return TargetContract(operation, parameters);
            }
            return true;
        }
        [Syscall("Neo.CrossChain.CreateTransaction")]
        public static extern bool CreateCrossChainTransaction(byte[] paraBytes);
    }
}
