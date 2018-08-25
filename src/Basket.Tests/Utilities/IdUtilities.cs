using NUnit.Framework;
using Basket.Api.Utilities;

namespace Basket.UnitTests.Utilities
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