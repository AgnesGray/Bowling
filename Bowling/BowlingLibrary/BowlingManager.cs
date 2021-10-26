using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        bool gameStarted { get; set; }


        public void StartGame(IEnumerable<string> playerNames)
        {
            if (playerNames.Count() < 2 || playerNames.Count() > 6)
            { 
                throw new PlayersNumberException("Players number must be between 2 and 6, inclusively."); 
            }
            else
            {
                if (ValidateNames(playerNames))
                    gameStarted = true;
                else 
                    throw new NamesNotUniqueException("Players names must be unique.");
            }
        }

        public bool ValidateNames(IEnumerable<string> playerNames)
        {
            //de implementat
            return false;
        }

        public void NextShot(int pils)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlayer> GetStanding()
        {
            throw new NotImplementedException();
        }

        //exemplu consumer
        public void writeInConsole()
        {
            Console.WriteLine("test");
        }
    }
}
