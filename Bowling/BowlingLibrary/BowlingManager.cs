using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        public int framesNumber { get; set; }
        public bool GameStarted { get; set; }
        

        public void StartGame(IEnumerable<string> playerNames)
        {
            ValidatePlayers(playerNames);

            SetPlayerAndFrames(framesNumber, playerNames); //metoda de setare player si frame-uri - apelare

            this.GameStarted = true;

            StartShoots();
        }


        public void ValidatePlayers(IEnumerable<string> players) {
            if (players.Count() < 2 || players.Count() > 6)
            {
                throw new PlayersNumberException("Players number must be between 2 and 6, inclusively.");
            }

            if (players.Distinct().Count() != players.Count())
            {
                throw new NamesNotUniqueException("Players names must be unique.");
            }
        }

        //metoda de setare player si frame-uri
        public void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames)
        {
            Dictionary<string, List<Frame>> playerFrames = new Dictionary<string, List<Frame>>();
            
            foreach (string p in playerNames)
            {
                var frames = new List<Frame>();
                for (int i=1; i<=framesNumber; i++) //?last frame
                {
                    frames.Add(new Frame());
                }

                playerFrames.Add(p, frames);
            }
        }

        //StartShoots();
        //face NextShot pana la last frame of last player
        //lucreaza cu obiectul frame
        // verifica ca e strike sau spare
        // si seteaza in obiectul frame
        public void StartShoots()
        {

            this.GameStarted = false;
        }


        public void NextShot(int pins)
        {
            if (!this.GameStarted) 
            {
                throw new GameStateException("Game not started.");
            }

            if ((pins < 1) || (pins > 10)) //add varianta pt 2nd throw in a frame
            {
                throw new PinsNumberException("Invalid number of pins.");
            }   
            
            //ce face???
        }

        public IEnumerable<IPlayer> GetStanding()
        {
            if (this.GameStarted)
            {
                throw new GameStateException("Game should be finished.");
            }

            var playersList = new List<IPlayer>(); //populate

            return playersList.OrderBy(o => o.TotalScore).ToList();
        }
       
    }
}
