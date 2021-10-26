using System;
using System.Runtime.Serialization;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class GameStateException : Exception
    {
        public GameStateException() { }
        public GameStateException(string message)
            : base(message) { }
        protected GameStateException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
