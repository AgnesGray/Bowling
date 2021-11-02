using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    interface ILastFrame
    {
        public int? ThirdShot { get; set; }

        public abstract void SaveThirdShot(int pins);
    }
}
