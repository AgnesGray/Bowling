using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public abstract class FrameModel
    {
        public int? FirstShot { get; set; }
        public int? SecondShot { get; set; }


        public bool isStrike()
        {
            if (FirstShot == 10)
                return true;
            else
                return false;
        }

        public bool isSpare()
        {
            int? TotalPins = FirstShot + SecondShot;

            if (TotalPins == 10 && !isStrike())
                return true;
            else
                return false;
        }

        public abstract bool checkShotsSaved(); 
        public abstract void SaveFirstShot(int pins);
        public abstract void SaveSecondShot(int pins);
        
        public abstract int? shotsSum();
    }
}
