using NUnit.Framework;

namespace BowlingApplication.Test
{
    [TestFixture]
    public class BowlingManagerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        

        [Test]
        public void Test1()
        {
            var result = 2 + 2;
            Assert.AreEqual(4, result);
        }
    }
}