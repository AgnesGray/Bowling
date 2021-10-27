using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        public bool GameStarted { get; set; }
        //public IEnumerable<IPlayer> PlayerNames { get; set; }

        public void StartGame(IEnumerable<string> playerNames);
        public void NextShot(int pins);
        public IEnumerable<IPlayer> GetStanding();


        //new
        public void ValidatePlayerNames(IEnumerable<string> players);
        public void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames);
        public void StartShoots();
    }
}
