using System;
using IdGen;

namespace Basket.Api.Utilities
{
    public static class IdUtilities
    {
        private static IdGenerator _generator = new IdGenerator(0);
        public static long GenerateId()
        {
            return _generator.CreateId();
        }
    }
}