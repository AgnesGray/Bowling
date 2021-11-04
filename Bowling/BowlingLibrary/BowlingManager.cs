﻿using System;
using System.Collections.Generic;
using System.Linq;
using BowlingLibrary.Exceptions;

namespace BowlingLibrary
{
    public class BowlingManager : IBowlingManager
    {
        private int framesNumber { get; set; }
        private bool GameStarted { get; set; }

        public Dictionary<string, List<Frame>> gameBoard { get; }

        protected int frameOrderNumber = 0;
        protected int maximShot = 10;


        public BowlingManager(int frames)
        {
            framesNumber = frames;
            this.GameStarted = false;
            gameBoard = new Dictionary<string, List<Frame>>();
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
            foreach (string p in playerNames)
            {
                var frames = new List<Frame>();
                for (int i = 1; i < framesNumber; i++)
                {
                    frames.Add(new Frame());
                }
                frames.Add(new LastFrame());

                //implement last shot
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
            bool saved = false;

            string last = gameBoard.Keys.Last();

            foreach (var framesList in gameBoard.Values)
            {
                if (framesList[frameOrderNumber].FirstShot == null)
                {
                    framesList[frameOrderNumber].SaveFirstShot(pins);
                    saved = true;

                    if (pins != 10)
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

                    break;
                }

                else if (frameOrderNumber == framesNumber - 1 && (framesList[frameOrderNumber] as LastFrame).ThirdShot == null)//if last turn and first 2 shots already saved
                {
                    SaveLast(pins, framesList[frameOrderNumber]);

                    saved = true;
                    break;
                }

            }

            if ((gameBoard[last][framesNumber - 1] as LastFrame).ThirdShot != null)
            {
                this.GameStarted = false;
            }
            else if (!saved /*&& this.GameStarted*/)
            {
                frameOrderNumber++;
                NextShot(pins);
            }
        }


        private void SaveLast(int pins, IFrame lastFrame)
        {
            //daca suma <10 - 0
            if (lastFrame.ShotsSum() < 10)
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


        public (int, int?) ComputeScore(List<Frame> Frames, int i, int countDouble, int? currentFrameScore)
        {
            if (Frames[i].IsStrike() && (i != framesNumber - 1)) //not last frame
            {
                countDouble = countDouble < 3 ? countDouble + 1 : countDouble;
                currentFrameScore += 10 * countDouble;

            }
            else
            {
                currentFrameScore += Frames[i].ShotsSum();

                if (countDouble != 0)
                {
                    currentFrameScore += Frames[i].ShotsSum();

                    if (i > 1 && countDouble > 1)
                    {
                        currentFrameScore += Frames[i].FirstShot;
                    }
                }

                countDouble = 0;
            }

            if (i > 0 && Frames[i-1].IsSpare())
            {
                currentFrameScore += Frames[i].FirstShot;
            }

            if (i == framesNumber - 1) //e ultimul frame
            {
                currentFrameScore = currentFrameScore + (Frames[i] as LastFrame).ThirdShot;
            }

            (int, int?) result = (countDouble, currentFrameScore);
                
            return result;
        }

    }
}
