﻿using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace CrossChainContract
{
    class Contract1 : SmartContract
    {
        public static bool Main()
        {
            Storage.Put("Hello", "World");
            return true;
        }
    }
}
