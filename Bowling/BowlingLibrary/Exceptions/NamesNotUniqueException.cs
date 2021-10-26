using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class NamesNotUniqueException : Exception
    {
        public NamesNotUniqueException() { }
        public NamesNotUniqueException(string message)
        : base(message) { }

        public NamesNotUniqueException(string message, Exception inner)
            : base(message, inner) { }
    }
}
