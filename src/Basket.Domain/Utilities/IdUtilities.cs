using IdGen;

namespace Basket.Domain.Utilities
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