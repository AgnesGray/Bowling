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

        [Test]
        public void StartShoots_Test()
        {
            var Players = new List<string>()
            {
                "Player1",
                "Player2",
                "Player3",
            };
            bowlingManager.SetPlayerAndFrames(3, Players);
            bowlingManager.StartShoots();

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
        public void NextShot_ValidPinsNumber()
        {
            //Arrange
            //Act
            bowlingManager.GameStarted = true;

            bowlingManager.NextShot(8);

            Assert.Pass();
        }



        //method GetStanding() - returns an ordered list of players, based on total score        
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
        public void GetStanding_TestResult()
        {
            //Arrange
           

            

            //Act
            bowlingManager.GameStarted = false;
            var Players = new List<string>()
            {
                "A",
                "B",
                "C",
            };
            bowlingManager.SetPlayerAndFrames(3, Players);
            bowlingManager.StartShoots();

            var result = bowlingManager.GetStanding().ToList();

            for (int i=1; i<3; i++)
            {
                var first = result[i-1].TotalScore;
                var next = result[i].TotalScore;
                
                Assert.IsTrue(first < next, "The standing is not ordered right.");
            }           
        }
    }
}