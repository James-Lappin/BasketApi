using Basket.Domain.Utilities;
using NUnit.Framework;

namespace Basket.Tests.Utilities
{
    public class IdUtilitiesTests
    {
        [Test]
        public void GenerateMultipleIds()
        {
           var id1 = IdUtilities.GenerateId();
           var id2 = IdUtilities.GenerateId();

           Assert.That(id1, Is.Not.EqualTo(id2));
        }
    }
}