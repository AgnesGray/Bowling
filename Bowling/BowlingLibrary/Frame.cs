using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public class Frame : IFrame
    {
        public int? FirstShot { get; protected set; }
        public int? SecondShot { get; protected set; }


        public bool IsStrike()
        {
            if (FirstShot == 10)
                return true;
            else
                return false;
        }

        public bool IsSpare()
        {
            int? TotalPins = FirstShot + SecondShot;

            if (TotalPins == 10 && !IsStrike())
                return true;
            else
                return false;
        }

        
        public virtual void SaveFirstShot(int pins)
        {
            if (FirstShot == null)
            {
                FirstShot = pins;
            }

            if (pins == 10)
            {
                SecondShot = 0;
            }
        }

        public virtual void SaveSecondShot(int pins)
        {
            if (SecondShot == null && FirstShot != null)//
            {
                SecondShot = pins;
            }
        }

        public virtual int? FirstAndSecondShotsSum()
        {
            return FirstShot + SecondShot;
        }
    }
}
