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



        //method StartGame - receives a list of names
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

        //test peste 6
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

        //test not unique players names
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
        /*

        //method NextShot(int pils) - receives a value between 0 and 9 which represents how many pils are hit in a turn.
        //should throw exception if the game has not started yet
        //should throw exception if game has finished
        //should throw if the value of "pils" is not correct
        [Test]
        public void NextShot_Test_ShouldReturn()
        {
            //Arrange
            var bowlingM = new BowlingManager();
            //Act
            var result = bowlingM.GetStanding();
            //Assert
            Assert.Pass();
        }



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