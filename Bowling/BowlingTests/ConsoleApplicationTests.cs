using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConsoleApplication;

namespace BowlingTests
{
    [TestFixture]
    class ConsoleApplicationTests
    {
        ConsoleApplicationClass c = new ConsoleApplicationClass();

        
        [Test]
        public void writeConsole_TestOk()
        {
            try
            {
                var read = c.readConsole();
                Console.WriteLine(read);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

    }
}
