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
            frame.SaveFirstShot(8);
            Assert.IsFalse(frame.IsStrike());
        }

        [Test]
        public void isStrike_TestReturn_True()
        {
            frame.SaveFirstShot(10);
            Assert.IsTrue(frame.IsStrike());
        }

        
        [Test]
        public void isSpare_TestReturn_False()
        {
            frame.SaveFirstShot(0);
            frame.SaveSecondShot(8);
            Assert.IsFalse(frame.IsSpare());
        }

        [Test]
        public void isSpare_TestReturn_True()
        {
            frame.SaveFirstShot(2);
            frame.SaveSecondShot (8);
            Assert.IsTrue(frame.IsSpare());
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
            frame.SaveFirstShot(2);
            frame.SaveFirstShot(5);
            Assert.AreEqual(2, frame.FirstShot);
        }


        [Test]
        public void SaveSecondShot_Test_Save()
        {
            frame.SaveFirstShot(1);
            frame.SaveSecondShot(5);
            Assert.AreEqual(5, frame.SecondShot);
        }

        [Test]
        public void SaveSecondShot_Test_NotSave()
        {
            //frame.FirstShot = null;
            frame.SaveFirstShot(5);
            Assert.AreEqual(null, frame.SecondShot);
        }

    }
}
