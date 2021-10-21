using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApplication
{
    interface IFrame
    {
        int firstShot { get; }
        int secondShot { get; }

        const int pinsTotalNumber = 10;

        bool isStrike();
        bool isSpare(); 
    }
}
