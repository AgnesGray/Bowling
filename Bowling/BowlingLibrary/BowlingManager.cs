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

        public Dictionary<string, List<Frame>> playerFrames { get; set; }


        public BowlingManager(int frames)
        {
            framesNumber = frames;
            this.GameStarted = false; //game not started
        }

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

        
        public void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames)
        {
            playerFrames = new Dictionary<string, List<Frame>>();
            
            foreach (string p in playerNames)
            {
                var frames = new List<Frame>();
                for (int i=1; i<=framesNumber; i++)
                {
                    frames.Add(new Frame());
                }

                //implement last shot
                playerFrames.Add(p, frames);
            }
        }

               
        public void StartShoots()
        {
            
            foreach (var playerFrame in playerFrames.Values)
            {
                for (int i=0; i<framesNumber; i++ )
                {
                    
                    var num = new Random();
                    int pins = num.Next(10);

                    playerFrame[i].SaveFirstShot(pins);
                    

                    if (pins < 10)
                    {
                        pins = num.Next(10-pins);
                        playerFrame[i].SaveSecondShot(pins);
                    }
                }

                //implement last shot
            }

            this.GameStarted = false;
        }


        public void NextShot(int pins)
        {
            if (!this.GameStarted) 
            {
                throw new GameStateException("Game not started.");
            }

            if ((pins < 1) || (pins > 10))
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

            var playersScoreList = new List<IPlayer>();

            
            foreach (var (key,playerFrame) in playerFrames)
            {
                int countDouble = 0;
                int? currentFrameScore = 0;
                for (int i = 0; i < framesNumber; i++)
                {
                    
                    if (playerFrame[i].isStrike())
                    {
                        countDouble++;
                        currentFrameScore += 10 * countDouble;
                    }

                    else
                    {
                        int? firstAndSecond = playerFrame[i].FirstShot + playerFrame[i].SecondShot;

                        currentFrameScore += firstAndSecond;

                        if (countDouble != 0) {
                            currentFrameScore += firstAndSecond;
                        }
                        else if (i>0 && playerFrame[i-1].isSpare())
                        {
                            currentFrameScore += playerFrame[i].FirstShot;

                        }

                        countDouble = 0;
                    }
                }


                playersScoreList.Add(new Player(key, currentFrameScore));
            }

           
            var result = new List<IPlayer>();
            result = playersScoreList.OrderBy(o => o.TotalScore).ToList();
            
            return result;
        }
       
    }
}
