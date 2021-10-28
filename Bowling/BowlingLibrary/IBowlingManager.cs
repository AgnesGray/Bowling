using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        public bool GameStarted { get; set; }
        public Dictionary<string, List<Frame>> playerFrames { get; set; }

        public void StartGame(IEnumerable<string> playerNames);
        public void NextShot(int pins);
        public IEnumerable<IPlayer> GetStanding();


        //new
        public void ValidatePlayers(IEnumerable<string> players);
        public void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames);
        public void StartShoots();
    }
}
