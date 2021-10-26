using System.Collections.Generic;

namespace BowlingLibrary
{
    public interface IBowlingManager
    {
        void StartGame(IEnumerable<string> playerNames);
        void NextShot(int pils);
        IEnumerable<IPlayer> GetStanding();
    }
}
