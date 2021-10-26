using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        public bool gameStarted { get; set; }

        void StartGame(IEnumerable<string> playerNames);
        void NextShot(int pils);
        IEnumerable<IPlayer> GetStanding();
    }
}
