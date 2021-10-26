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
    }
}
