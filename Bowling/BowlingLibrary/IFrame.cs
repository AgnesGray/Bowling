using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public interface IFrame
    {
        public int? FirstShot { get; set; }
        public int? SecondShot { get; set; }

        public bool isStrike();
        public bool isSpare();
        public void SaveFirstShot(int pins);
        public void SaveSecondShot(int pins);
    }
}