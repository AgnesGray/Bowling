using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {       
        public Dictionary<string, List<Frame>> gameBoard { get; }        

        public void StartGame(IEnumerable<string> playerNames);
        public void NextShot(int pins);
        public IEnumerable<IPlayer> GetStanding();

    }
}
