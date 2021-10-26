using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BowlingApplication.Test
{
    [TestFixture]
    public class Test1
    {
        //private IBowlingManager bowlingManager;


        [SetUp]
        public void Setup()
        {
            //new object
            var playersList = new List<IPlayer>() { };
            //IBowlingManager newGame = new BowlingManager();
        }


        //exception on incorect players number - less then 2
        [Test]
        public void TestSomething()
        {
            //Arrange
            //Act
            //Assert
            Assert.
        }

        //exception on incorect players number - more then 6


        //exception on names not unique


        //exception on incorect frames number - less then 1


        //exception on incorect frames number - more then 10


    }
}
