using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        private int framesNumber { get; set; }
        private bool GameStarted { get; set; } 

        public Dictionary<string, List<Frame>> gameBoard { get; set; }

        protected int frameOrderNumber = 0;
        protected int maximShot = 10;

        public BowlingManager(int frames)
        {
            framesNumber = frames;
            this.GameStarted = false;
        }

        public void StartGame(IEnumerable<string> playerNames)
        {
            if (this.GameStarted)
            {
                throw new GameStateException("Game already started.");
            }

            ValidatePlayers(playerNames);

            SetPlayerAndFrames(framesNumber, playerNames);

            this.GameStarted = true;
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
            gameBoard = new Dictionary<string, List<Frame>>();

            foreach (string p in playerNames)
            {
                var frames = new List<Frame>();
                for (int i = 1; i <= framesNumber; i++)
                {
                    frames.Add(new Frame());
                }

                //implement last shot
                gameBoard.Add(p, frames);
            }
        }



        public void NextShot(int pins)
        {
            if (!this.GameStarted)
            {
                throw new GameStateException("Game not started.");
            }

            if ((pins < 0) || (pins > maximShot))
            {
                throw new PinsNumberException("Invalid number of pins.");
            }

            SavePins(pins);
        }

        public void SavePins(int pins)
        {
            bool saved = false;

            
            foreach (var framesList in gameBoard.Values)
            {
                if (framesList[frameOrderNumber].FirstShot == null)
                {
                    framesList[frameOrderNumber].SaveFirstShot(pins);
                    saved = true;

                    if (pins == 10)
                    {
                        framesList[frameOrderNumber].SaveSecondShot(0);
                    }
                    else
                    {
                        maximShot = 10 - pins;
                    }
                    break;
                }
                else if (framesList[frameOrderNumber].SecondShot == null)
                {
                    framesList[frameOrderNumber].SaveSecondShot(pins);
                    saved = true;
                    maximShot = 10;
                    //daca e ultima tura -> frame list e ultima - checked last
                   
                    break;
                }
            }

            if (!saved)
            {
                if (frameOrderNumber == framesNumber - 1)
                {
                    this.GameStarted = false;
                }
                else
                {
                    frameOrderNumber++;
                    NextShot(pins);
                }
            }
        }

        public IEnumerable<IPlayer> GetStanding()
        {
            if (this.GameStarted)
            {
                throw new GameStateException("Game should be finished.");
            }

            var playersScoreList = new List<IPlayer>();

            
            foreach (var (key, gameBoard) in gameBoard)
            {
                int countDouble = 0;
                int? currentFrameScore = 0;
                for (int i = 0; i < framesNumber; i++)
                {
                    
                    if (gameBoard[i].isStrike())
                    {
                        countDouble = countDouble<3 ? countDouble+1 : countDouble;
                        currentFrameScore += 10 * countDouble;
                    }

                    else
                    {
                        int? firstAndSecond = gameBoard[i].FirstShot + gameBoard[i].SecondShot;

                        currentFrameScore += firstAndSecond;

                        if (countDouble != 0) {
                            currentFrameScore += firstAndSecond;

                            if (i>1 && countDouble > 1)
                            {
                                currentFrameScore += gameBoard[i].FirstShot;
                            }
                        }
                       
                        countDouble = 0;
                    }

                    if (i > 0 && gameBoard[i - 1].isSpare())
                    {
                        currentFrameScore += gameBoard[i].FirstShot;
                    }
                }

                playersScoreList.Add(new Player(key, currentFrameScore));
            }

            var result = playersScoreList.OrderBy(o => o.TotalScore).ToList();
            
            return result;
        }     
    }
}
