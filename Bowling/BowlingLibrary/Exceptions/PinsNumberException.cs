using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class PinsNumberException : Exception
    {
        public PinsNumberException() { }
        public PinsNumberException(string message)
        : base(message) { }

        public PinsNumberException(string message, Exception inner)
            : base(message, inner) { }
    }
}
