using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        public bool GameStarted { get; set; }
        public IEnumerable<IPlayer> PlayerNames { get; set; }

        void StartGame(IEnumerable<string> playerNames);
        void NextShot(int pils);
        IEnumerable<IPlayer> GetStanding();
    }
}
