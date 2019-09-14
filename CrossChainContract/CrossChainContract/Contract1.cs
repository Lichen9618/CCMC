using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace CrossChainContract
{
    class Contract : SmartContract
    {
        delegate object DynCall(string method, object[] args);

        public static bool Main(string method, object[] args)
        {            
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
