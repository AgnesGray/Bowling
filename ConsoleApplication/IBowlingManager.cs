using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApplication
{
    interface IBowlingManager
    {
        int FramesNumber { get; set; } // ! e ok acest set?
        void StartGame(IEnumerable<string> playerNames);
        void NextShot(int pils);
        IEnumerable<IPlayer> GetStanding();
    }
}
