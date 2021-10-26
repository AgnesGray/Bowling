using System;
using System.Runtime.Serialization;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class NamesNotUniqueException : Exception
    {
        public NamesNotUniqueException() { }
        public NamesNotUniqueException(string message)
            : base(message) { }
        protected NamesNotUniqueException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
