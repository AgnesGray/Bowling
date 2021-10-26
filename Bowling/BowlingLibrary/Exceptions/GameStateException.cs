using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class GameStateException : Exception
    {
        public GameStateException() { }
        public GameStateException(string message)
        : base(message) { }

        public GameStateException(string message, Exception inner)
            : base(message, inner) { }
    }
}
