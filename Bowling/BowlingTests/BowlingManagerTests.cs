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
        public void StartGame_LessThenTwo_Exception() //should return exception
        {
            //Arrange
            var Players = new List<string>();

            //Act
            //Assert
            Assert.Throws<PlayersNumberException>(() =>
            {
                bowlingManager.StartGame(Players);
            });
        }

        [Test]
        public void StartGame_MoreThenSix_Exception() //should return exception
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
                bowlingManager.StartGame(Players);
            });
        }

        [Test]
        public void StartGame_NamesNotUnique_Exception() //should return exception
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
                bowlingManager.StartGame(Players);
            });
        }



        //method NextShot(int pils) - receives a value between 0 and 9 which represents how many pils are hit in a turn.       
        //[Test]
        //public void NextShot_GameNotStarted_Exception()//should throw exception | game has finished - e ok sa tratez tot aici ?
        //{
        //    //Arrange
        //    //Act
        //    bowlingManager.gameStarted = false;

        //    //Assert
        //    Assert.Throws<GameStateException>(() =>
        //    {
        //        bowlingManager.NextShot(2);
        //    });
        //}

        //[Test]
        //public void NextShot_InvalidPillsNumber_Exception()//should throw exception
        //{            
        //    Assert.Throws<GameStateException>(() =>
        //    {
        //        bowlingManager.NextShot(12);
        //    });
        //}


        /*
        //method GetStanding() - returns an ordered list of players, based on total score
        //should throw if the game has not finished
        [Test]
        public void GetStanding_TestGameNotFinished_ShouldReturnException()
        {
            //Arrange
            var bowlingM = new BowlingManager();
            //Act
            var result = bowlingM.GetStanding();
            //Assert
            Assert.Pass();
        }

        */
    }
}