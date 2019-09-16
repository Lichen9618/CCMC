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
            if (method == "processCrossChainTx")
            {
                byte[] Target = (byte[])args[0];
                string operation = (string)args[1];
                object[] parameters = new object[args.Length - 2];
                args.CopyTo(parameters, 2);
                return ProcessCrossChainTx(Target, operation, parameters);
            }
            return true;
        }

        private static object ProcessCrossChainTx(byte[] Target, string operation, object[] args)
        {
            if (Target.Length != 20) return false;
            DynCall TargetContract = (DynCall)Target.ToDelegate();
            return TargetContract(operation, args);
        }
    }
}
