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

        private IBowlingManager bowlingManager;

        [SetUp]
        public void MainSetup()
        {
            bowlingManager = new BowlingManager(3);
        }


        [Test]
        public void ValidatePlayerNames_LessThenTwo_Exception() //should return exception
        {
            //Arrange
            var Players = new List<string>();

            //Act
            //Assert
            Assert.Throws<PlayersNumberException>(() =>
            {
                bowlingManager.ValidatePlayers(Players);
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

            //Act
            //Assert
            Assert.Throws<PlayersNumberException>(() =>
            {
                bowlingManager.ValidatePlayers(Players);
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

            //Act
            //Assert
            Assert.Throws<NamesNotUniqueException>(() =>
            {
                bowlingManager.ValidatePlayers(Players);
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

            //Act
            //Assert
            Assert.DoesNotThrow(() => bowlingManager.ValidatePlayers(Players));
        }


        [Test]
        public void SetPlayerAndFrames_Test()
        {
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            bowlingManager.SetPlayerAndFrames(3, Players);
            Assert.Pass();
        }

        

        //method NextShot(int pils) - receives a value between 0 and 9 which represents how many pils are hit in a turn.       
        [Test]
        public void NextShot_GameNotStarted_Exception()
        {
            //Arrange
            //Act
            bowlingManager.GameStarted = false;

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
            //Act
            bowlingManager.GameStarted = true;

            Assert.Throws<PinsNumberException>(() =>
            {
                bowlingManager.NextShot(12);
            });
        }



        [Test]
        public void NextShot_Save()
        {
            //Arrange
            bowlingManager.GameStarted = true;

            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            bowlingManager.SetPlayerAndFrames(3, Players);

            var list = bowlingManager.gameBoard["Player1"];

            int? beforeShotNull = list[0].FirstShot;
            Assert.AreEqual(null, beforeShotNull);

            //Act
            bowlingManager.NextShot(10);

            int? afterShotValue = list[0].FirstShot;
            Assert.AreEqual(10, afterShotValue);
            Assert.AreEqual(0, list[0].SecondShot);
            Assert.IsTrue(list[0].isStrike());
        }


        [Test]
        public void NextShot_Save2()
        {
            //Arrange
            bowlingManager.GameStarted = true;

            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            bowlingManager.SetPlayerAndFrames(3, Players);

            //Act
            bowlingManager.StartGame(Players);
            bowlingManager.NextShot(9);
            bowlingManager.NextShot(1);

            int? first = bowlingManager.gameBoard["Player1"][0].FirstShot;
            int? second = bowlingManager.gameBoard["Player1"][0].SecondShot;
            Assert.AreEqual(9, first);
            Assert.AreEqual(1, second);
        }

        [Test]
        public void GetStanding_TestGameNotFinished_ShouldReturnException()
        {
            //Arrange
            //Act
            bowlingManager.GameStarted = true;

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
            //Act
            bowlingManager.GameStarted = false;
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };

            bowlingManager.StartGame(Players);

            while (bowlingManager.GameStarted)
            {
                bowlingManager.NextShot(10);
            }
            
            var result = bowlingManager.GetStanding().ToList();

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(60, result[i].TotalScore);
            }
        }


        [Test]
        public void GetStanding_TestScores_AllTheSame()
        {
            //Arrange
            //Act
            bowlingManager.GameStarted = false;
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };

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

            //extra
            bowlingManager.NextShot(5);

            var result = bowlingManager.GetStanding().ToList();

            Assert.AreEqual(45, result[0].TotalScore);
            Assert.AreEqual(45, result[1].TotalScore);
            Assert.AreEqual(45, result[2].TotalScore);
        }

    }
}