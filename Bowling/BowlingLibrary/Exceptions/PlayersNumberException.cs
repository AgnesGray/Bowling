using System;
using System.Runtime.Serialization;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class PlayersNumberException : Exception
    {
        public PlayersNumberException(string message)
            : base(message) { }
        protected PlayersNumberException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
