using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public class Frame : IFrame
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

        
        public void SaveFirstShot(int pins)
        {
            if (FirstShot == null)
            {
                FirstShot = pins;
            }
        }

        public void SaveSecondShot(int pins)
        {
            if (SecondShot == null && FirstShot != null)
            {
                SecondShot = pins;//pins <= 10 - FirstShot
            }
        }

        public int? shotsSum()
        {
            return this.FirstShot + this.SecondShot;
        }
    }
}
