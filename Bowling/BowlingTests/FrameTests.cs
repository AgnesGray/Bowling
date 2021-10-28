using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BowlingLibrary;
using BowlingLibrary.Exceptions;

namespace BowlingTests
{
    [TestFixture]
    class FrameTests
    {

        private IFrame frame;

        [SetUp]
        public void MainSetup()
        {
            frame = new Frame();
        }


        [Test]
        public void isStrike_TestReturn_False()
        {
            frame.FirstShot = 8;
            Assert.IsFalse(frame.isStrike());
        }

        [Test]
        public void isStrike_TestReturn_True()
        {
            frame.FirstShot = 10;
            Assert.IsTrue(frame.isStrike());
        }

        
        [Test]
        public void isSpare_TestReturn_False()
        {
            frame.FirstShot = 0;
            frame.SecondShot = 8;
            Assert.IsFalse(frame.isSpare());
        }

        [Test]
        public void isSpare_TestReturn_True()
        {
            frame.FirstShot = 2;
            frame.SecondShot = 8;
            Assert.IsTrue(frame.isSpare());
        }


        [Test]
        public void SaveFirstShot_Test_Save()
        {
            frame.SaveFirstShot(5);
            Assert.AreEqual(5, frame.FirstShot);
        }

        [Test]
        public void SaveFirstShot_Test_NotSave()
        {
            frame.FirstShot = 2;
            frame.SaveFirstShot(5);
            Assert.AreEqual(2, frame.FirstShot);
        }


        [Test]
        public void SaveSecondShot_Test_Save()
        {
            frame.FirstShot = 1;
            frame.SaveSecondShot(5);
            Assert.AreEqual(5, frame.SecondShot);
        }

        [Test]
        public void SaveSecondShot_Test_NotSave()
        {
            frame.FirstShot = null;
            frame.SaveFirstShot(5);
            Assert.AreEqual(null, frame.SecondShot);
        }
    }
}
