using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        public bool GameStarted { get; set; }
        public IEnumerable<IPlayer> PlayerNames { get; set; }


        public void StartGame(IEnumerable<string> playerNames)
        {
            if (playerNames.Count() < 2 || playerNames.Count() > 6)
            { 
                throw new PlayersNumberException("Players number must be between 2 and 6, inclusively."); 
            }
           
            if (playerNames.Distinct().Count() != playerNames.Count())
            { 
                throw new NamesNotUniqueException("Players names must be unique.");
            }

            this.GameStarted = true;
        }

        public void NextShot(int pins)
        {
            if (this.GameStarted) 
            {
                throw new GameStateException("Game not started.");
            }

            if ((pins < 1) || (pins > 10)) //add varianta pt 2nd throw in a frame
            {
                throw new PinsNumberException("Invalid number of pins.");
            }       
        }

        public IEnumerable<IPlayer> GetStanding()
        {
            if (this.GameStarted)
            {
                throw new GameStateException("Game should be finished.");
            }

            //order List
            this.OrderPlayersByTotalScore();

            return this.PlayerNames;
        }

        public void OrderPlayersByTotalScore()
        {
            this.PlayerNames.OrderBy(o => o.TotalScore).ToList();
        }


        //exemplu consumer
        public void writeInConsole()
        {
            Console.WriteLine("test");
        }
    }
}
