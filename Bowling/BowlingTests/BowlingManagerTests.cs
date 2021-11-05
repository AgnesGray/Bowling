using NUnit.Framework;
using BowlingLibrary;
using BowlingLibrary.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BowlingTests
{
    [TestFixture]
    public class BowlingManagerTests
    {

        [Test]
        public void ValidatePlayerNames_LessThenTwo_Exception() //should return exception
        {
            //Arrange
            var Players = new List<string>();
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            //Assert
            Assert.Throws<PlayersNumberException>(() =>
            { 
                bowlingManager.StartGame(Players);
            });
        }

        [Test]
        public void ValidatePlayerNames_MoreThenSix_Exception() //should return exception
        {
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
                "Player4",
                "Player5",
                "Player6",
                "Player7",
                "Player8",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            //Assert
            Assert.Throws<PlayersNumberException>(() =>
            {
                bowlingManager.StartGame(Players);
            });
        }

        [Test]
        public void ValidatePlayerNames_NamesNotUnique_Exception() //should return exception
        {
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player1",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            //Assert
            Assert.Throws<NamesNotUniqueException>(() =>
            {
                bowlingManager.StartGame(Players);
            });
        }

        [Test]
        public void ValidatePlayerNames_NamesUnique() //should return exception
        {
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            Assert.DoesNotThrow(() => bowlingManager.StartGame(Players));
        }



        [Test]
        public void NextShot_GameNotStarted_Exception()
        {
            //Arrange
            //Act
            var bowlingManager = new BowlingManager(3);

            //Assert
            Assert.Throws<GameStateException>(() =>
            {
                bowlingManager.NextShot(2);
            });
        }

        [Test]
        public void NextShot_InvalidPinsNumber_Exception()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            bowlingManager.StartGame(Players);

            //Assert
            Assert.Throws<PinsNumberException>(() =>
            {
                bowlingManager.NextShot(12);
            });
        }

        [Test]
        public void NextShot_Save()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };

            BowlingManager bowlingManager = new BowlingManager(3);

            bowlingManager.StartGame(Players);
            //bowlingManager.SetPlayerAndFrames(3, Players);

            var list = bowlingManager.gameBoard["Player1"];

            int? beforeShotNull = list[0].FirstShot;
            Assert.AreEqual(null, beforeShotNull);

            //Act
            bowlingManager.NextShot(10);

            int? afterShotValue = list[0].FirstShot;
            Assert.AreEqual(10, afterShotValue);
            Assert.AreEqual(0, list[0].SecondShot);
            Assert.IsTrue(list[0].IsStrike());
        }

        [Test]
        public void NextShot_Save2()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);
            bowlingManager.StartGame(Players);
            
            //Act
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            int? first = bowlingManager.gameBoard["Player1"][0].FirstShot;
            int? second = bowlingManager.gameBoard["Player1"][0].SecondShot;

            //Assert
            Assert.AreEqual(9, first);
            Assert.AreEqual(1, second);
        }

        [Test]
        public void NextShot_TestFinishGame()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            bowlingManager.StartGame(Players);

            //1st
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(10);
            //2nd
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(10);
            //3rd
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(0);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(2);
            bowlingManager.NextShot(2);

            Assert.DoesNotThrow(() => bowlingManager.GetStanding());
        }



        [Test]
        public void GetStanding_TestGameNotFinished_ShouldReturnException()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);
            bowlingManager.StartGame(Players);

            //Assert
            Assert.Throws<GameStateException>(() =>
            {
                bowlingManager.GetStanding();
            });
        }

        [Test]
        public void GetStanding_TestScores_AllStrikes()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            bowlingManager.StartGame(Players);

            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);

            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);

            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);

            Assert.Throws<GameStateException>(() =>
            {
                bowlingManager.NextShot(10);
            });

            var result = bowlingManager.GetStanding().ToList();

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(90, result[i].TotalScore);
            }
        }

        [Test]
        public void GetStanding_TestScores_AllTheSame()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            bowlingManager.StartGame(Players);

            //first round
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            //second round
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);
            //third round
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);

            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);
            bowlingManager.NextShot(5);


            var result = bowlingManager.GetStanding().ToList();

            Assert.AreEqual(50, result[0].TotalScore);
            Assert.AreEqual(50, result[1].TotalScore);
            Assert.AreEqual(50, result[2].TotalScore);
        }

        [Test]
        public void GetStanding_TestScores_AllDifferent()
        {
            BowlingManager bowlingManager2 = new BowlingManager(10);
            //Arrange
            //Act

            var Players = new List<string>()
            {
                "Player1",
                "Player2",
            };

            bowlingManager2.StartGame(Players);

            //1st
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(1);
            //2nd
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //3rd
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //4th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //5th
            bowlingManager2.NextShot(8);
            bowlingManager2.NextShot(2);
            bowlingManager2.NextShot(8);
            bowlingManager2.NextShot(2);
            //6th
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            //7th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(1);
            //8th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(1);
            //9th
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(3);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(1);
            //10th
            bowlingManager2.NextShot(0);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(0);
            bowlingManager2.NextShot(1);

            var result = bowlingManager2.GetStanding().ToList();

            Assert.AreEqual(117, result[0].TotalScore);
            Assert.AreEqual(185, result[1].TotalScore);
        }

        [Test]
        public void GetStanding_TestScores_TwoPlayers_DifferentShoots()
        {
            //Arrange
            BowlingManager bowlingManager2 = new BowlingManager(9);
            
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
            };

            //Act
            bowlingManager2.StartGame(Players);

            //1st
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //2nd
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            //3rd
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            //4th
            bowlingManager2.NextShot(7);
            bowlingManager2.NextShot(2);
            bowlingManager2.NextShot(7);
            bowlingManager2.NextShot(1);
            //5th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //6th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //7th
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //8th
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            //9th
            bowlingManager2.NextShot(8);
            bowlingManager2.NextShot(2);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(8);
            bowlingManager2.NextShot(2);
            bowlingManager2.NextShot(1);

            var result = bowlingManager2.GetStanding().ToList();

            Assert.AreEqual(168, result[0].TotalScore);
            Assert.AreEqual(169, result[1].TotalScore);
        }


        [Test]
        public void SaveLast_Test()
        {
            //Arrange
            BowlingManager bowlingManager2 = new BowlingManager(3);

            var Players = new List<string>()
            {
                "Player1",
                "Player2",
            };
            bowlingManager2.StartGame(Players);
            
            //Act

            //1st
            bowlingManager2.NextShot(10);
            bowlingManager2.NextShot(10);
            //2nd
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(9);
            bowlingManager2.NextShot(1);
            //3rd
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(1);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);
            bowlingManager2.NextShot(5);

            var thirdShot = (bowlingManager2.gameBoard["Player2"][2] as LastFrame).ThirdShot;

            //Assert
            Assert.AreEqual(5, thirdShot);
        }

        [Test]
        public void GetStanding_TestScores_3PlayersStrikeVariants()
        {
            //Arrange
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            BowlingManager bowlingManager = new BowlingManager(3);

            //Act
            bowlingManager.StartGame(Players);

            //first round
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            //second round
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            //third round
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(10);
            bowlingManager.NextShot(1);

            bowlingManager.NextShot(10);
            bowlingManager.NextShot(1);
            bowlingManager.NextShot(9);

            bowlingManager.NextShot(1);
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);


            var result = bowlingManager.GetStanding().ToList();

            Assert.AreEqual(52, result[0].TotalScore);
            Assert.AreEqual(71, result[1].TotalScore);
            Assert.AreEqual(81, result[2].TotalScore);
        }
    }
}