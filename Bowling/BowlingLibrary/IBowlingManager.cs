using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        public bool GameStarted { get; set; }
        public IEnumerable<IPlayer> PlayerNames { get; set; }

        void StartGame(IEnumerable<string> playerNames);
        void NextShot(int pins);
        IEnumerable<IPlayer> GetStanding();

        //e ok?
        public void OrderPlayersByTotalScore();

    }
}
