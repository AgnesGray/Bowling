using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class InvalidPinsNumber : Exception
    {
        public InvalidPinsNumber() { }
        public InvalidPinsNumber(string message)
        : base(message) { }

        public InvalidPinsNumber(string message, Exception inner)
            : base(message, inner) { }
    }
}
