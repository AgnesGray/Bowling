using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        private int framesNumber;
        private bool GameStarted;

        public Dictionary<string, List<Frame>> gameBoard { get; }

        private int frameOrderNumber = 0;
        private int maximShot = 10;


        public BowlingManager(int frames)
        {
            framesNumber = frames;
            gameBoard = new Dictionary<string, List<Frame>>();
        }


        public void StartGame(IEnumerable<string> playerNames)
        {
            if (GameStarted)
            {
                throw new GameStateException("Game already started.");
            }

            ValidatePlayers(playerNames);

            SetPlayerAndFrames(framesNumber, playerNames);

            GameStarted = true;
        }


        private void ValidatePlayers(IEnumerable<string> players) {
            if (players.Count() < 2 || players.Count() > 6)
            {
                throw new PlayersNumberException("Players number must be between 2 and 6, inclusively.");
            }

            if (players.Distinct().Count() != players.Count())
            {
                throw new NamesNotUniqueException("Players names must be unique.");
            }
        }


        private void SetPlayerAndFrames(int framesNumber, IEnumerable<string> playerNames)
        {
            foreach (string p in playerNames)
            {
                var frames = new List<Frame>();
                for (int i = 1; i < framesNumber; i++)
                {
                    frames.Add(new Frame());
                }
                frames.Add(new LastFrame());

                this.gameBoard.Add(p, frames);
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
            string last = gameBoard.Keys.Last();
            if ((gameBoard[last][frameOrderNumber].SecondShot != null) && ((framesNumber - 1) != frameOrderNumber))
            {
                frameOrderNumber++;
            }


            foreach (var framesList in gameBoard.Values)
            {
                if (framesList[frameOrderNumber].FirstShot == null)
                {
                    SaveFirst(framesList[frameOrderNumber], pins);
                    break;
                }
                
                else if (framesList[frameOrderNumber].SecondShot == null)
                {
                    framesList[frameOrderNumber].SaveSecondShot(pins);
                    maximShot = 10;                    
                    break;
                }

                else if (frameOrderNumber == framesNumber - 1 && (framesList[frameOrderNumber] as LastFrame).ThirdShot == null)
                {
                    SaveLast(pins, framesList[frameOrderNumber]);
                    break;
                }
            }

            if ((gameBoard[last][framesNumber - 1] as LastFrame).ThirdShot != null)
            {
                this.GameStarted = false;
            }          
        }



        private void SaveFirst(Frame frame, int pins)
        {
            frame.SaveFirstShot(pins);

            if (pins != 10)
            {
                maximShot = 10 - pins;
            }
        }


        private void SaveLast(int pins, IFrame lastFrame)
        {
            if (lastFrame.FirstAndSecondShotsSum() < 10)
            {
                (lastFrame as LastFrame).ThirdShot = 0;
            }
            else
            {
                (lastFrame as LastFrame).ThirdShot = pins;
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

                for (int i = 0; i <= framesNumber - 1; i++)
                {
                    (int, int?) currentScoreAndDouble = ComputeScore(gameBoard, i, countDouble, currentFrameScore);
                    countDouble = currentScoreAndDouble.Item1;
                    currentFrameScore = currentScoreAndDouble.Item2;
                }

                playersScoreList.Add(new Player(key, currentFrameScore));
            }

            var result = playersScoreList.OrderBy(o => o.TotalScore).ToList();

            return result;
        }     


        public (int, int?) ComputeScore(List<Frame> frames, int i, int countDouble, int? currentFrameScore)
        {
            if (frames[i].IsStrike() && (i != framesNumber - 1)) //not last frame
            {
                countDouble = countDouble < 3 ? countDouble + 1 : countDouble;
                currentFrameScore += 10 * countDouble;

            }
            else
            {
                currentFrameScore += frames[i].FirstAndSecondShotsSum();

                if (countDouble != 0)
                {
                    currentFrameScore += frames[i].FirstAndSecondShotsSum();

                    if (i > 1 && countDouble > 1)
                    {
                        currentFrameScore += frames[i].FirstShot;
                    }
                }

                countDouble = 0;
            }

            if (i > 0 && frames[i-1].IsSpare())
            {
                currentFrameScore += frames[i].FirstShot;
            }

            if (i == framesNumber - 1)
            {
                currentFrameScore += (frames[i] as LastFrame).ThirdShot;
            }

            (int, int?) result = (countDouble, currentFrameScore);
                
            return result;
        }

    }
}
