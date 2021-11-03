using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public interface IFrame
    {
        public int? FirstShot { get; }
        public int? SecondShot { get; }

        public bool IsStrike();
        public bool IsSpare();
        public void SaveFirstShot(int pins);
        public void SaveSecondShot(int pins);
        public int? ShotsSum();
    }
}