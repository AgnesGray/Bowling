using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApplication
{
    class Frame : IFrame
    {
        public int firstShot { get; }
        public int secondShot { get; }

        const int pinsTotalNumber = 10;
       

        public bool isStrike()
        {
            if (firstShot == pinsTotalNumber) return true; 
            return false;
        }

        public bool isSpare() 
        {
            if ( !isStrike() && (firstShot+secondShot) == pinsTotalNumber) return true;
            return false;
        }
        

        //methods - abstract
        //checkIfTurnDone
        //getFrameTotalPils ??
    }
}
