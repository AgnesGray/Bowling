using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApplication
{
    class BowlingManager : IBowlingManager
    {
        public int FramesNumber { get; set; }
        public bool gamePlaying { get; set; }


        public BowlingManager()
        {
            gamePlaying = false;
            FramesNumber = 3;
        }

        public void StartGame(IEnumerable<string> playerNames)
        {
            //check uniqueness
            //check number of players 2<=  <=6
            ValidatePlayers(playerNames);
            gamePlaying = true;            
        }

        public void ValidatePlayers(IEnumerable<string> playerNames)
        {
            throw new NotImplementedException();
        }

        public void NextShot(int pils)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IPlayer> GetStanding()
        {
            throw new NotImplementedException();
        }

        

        
    }
}
