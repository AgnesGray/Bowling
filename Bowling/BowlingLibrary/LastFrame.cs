using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public class LastFrame : Frame
    {
        public int? ThirdShot { get; set; }

        public override void SaveSecondShot(int pins)
        {
            if ( SecondShot == null && FirstShot != null)
            {
                SecondShot = pins;
            }

            if (FirstAndSecondShotsSum() < 10)
            {
                ThirdShot = 0;
            }
        }

        public override void SaveFirstShot(int pins)
        {
            if (FirstShot == null)
            {
                FirstShot = pins;
            }
        }

        //public override int? ShotsSum()
        //{
            
        //    return FirstShot + SecondShot + ThirdShot;
            
        //}
    }

}
