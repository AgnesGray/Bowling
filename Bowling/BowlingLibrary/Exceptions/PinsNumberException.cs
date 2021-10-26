using System;
using System.Runtime.Serialization;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class PinsNumberException : Exception
    {
        public PinsNumberException() { }
        public PinsNumberException(string message)
            : base(message) { }
        protected PinsNumberException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
