using NUnit.Framework;
using BowlingLibrary;
using BowlingLibrary.Exceptions;
using System.Collections.Generic;

namespace BowlingTests
{
    [TestFixture]
    public class BowlingManagerTests
    {
        
        private IBowlingManager bowlingManager;

        [SetUp]
        public void MainSetup()
        {
            bowlingManager = new BowlingManager();
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
            bowlingManager.StartShoots();
            Assert.Pass();
        }

        //method NextShot(int pils) - receives a value between 0 and 9 which represents how many pils are hit in a turn.       
        [Test]
        public void NextShot_GameNotStarted_Exception()//should throw exception | game has finished - e ok sa tratez tot aici ?
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
        public void NextShot_InvalidPinsNumber_Exception()//should throw exception
        {            
            Assert.Throws<GameStateException>(() =>
            {
                bowlingManager.NextShot(12);
            });
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

       
    }
}