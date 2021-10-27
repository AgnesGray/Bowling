using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        public int framesNumber;
        public bool GameStarted { get; set; }
        //public IEnumerable<IPlayer> PlayerNames { get; set; }
        private Dictionary<string, List<Frame>> playerFrames;

        public void StartGame(IEnumerable<string> playerNames)
        {
            ValidatePlayerNames(playerNames);

            SetPlayerAndFrames(framesNumber, playerNames); //metoda de setare player si frame-uri - apelare

            this.GameStarted = true;

            StartShoots();
        }


        public void ValidatePlayerNames(IEnumerable<string> playerNames) {
            if (playerNames.Count() < 2 || playerNames.Count() > 6)
            {
                throw new PlayersNumberException("Players number must be between 2 and 6, inclusively.");
            }

            if (playerNames.Distinct().Count() != playerNames.Count())
            {
                throw new NamesNotUniqueException("Players names must be unique.");
            }

            return;
        }

        //metoda de setare player si frame-uri
        public void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames)
        {
            playerFrames = new Dictionary<string, List<Frame>>();

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
        }

        public IEnumerable<IPlayer> GetStanding()
        {
            if (this.GameStarted)
            {
                throw new GameStateException("Game should be finished.");
            }

            
            var playersList = new List<IPlayer>();

            //add player name and score to the list 

            //foreach(var p in playerFrames)
            //{
            //    var player = new Player(playerFrames., playerFrames.Ge);
            //    var playersList.Add();
            //}

            playersList.OrderBy(o => o.TotalScore).ToList();

            return playersList;
        }
       
    }
}
