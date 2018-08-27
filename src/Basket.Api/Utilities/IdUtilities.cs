using System;
using IdGen;

namespace Basket.Api.Utilities
{
    public static class IdUtilities
    {
        private static readonly IdGenerator Generator = new IdGenerator(0);
        public static long GenerateId()
        {
            return Generator.CreateId();
        }
    }
}