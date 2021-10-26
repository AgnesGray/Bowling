using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary.Exceptions
{
    [Serializable]
    public class PlayersNumberException : Exception
    {
        public PlayersNumberException() { }
        public PlayersNumberException(string message)
        : base(message) { }
    }
}
