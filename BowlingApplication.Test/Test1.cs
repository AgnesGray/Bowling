using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BowlingApplication;

namespace BowlingApplication.Test
{
    [TestFixture]
    class Test1
    {
        private IBowlingManager bowlingManager;


        [SetUp]
        public void Setup()
        {
            //new object

            IBowlingManager newGame = new BowlingManager();
        }


        //se intampla ce trebuie cand vine numar incorect de participanti - 0
        [Test]
        public void TestSomething()
        {
            //
        }

        //se intampla ce trebuie cand vine numar incorect de participanti - 7


        //exceptie cand numele nu sunt unice


        //exceptie cand nr de frames e 0


        //exceptie cand nr de frames e >10


    }
}
