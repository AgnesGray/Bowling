using BowlingLibrary;
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BowlingManager bowlingManager = new BowlingManager(3);// 3 is the frames number
            ConsoleApplication c = new ConsoleApplication();

            
           

            //get players
            var playerNames = new List<string>();
            
                playerNames.Add("A");
                playerNames.Add("B");
                playerNames.Add("C");


            
            bowlingManager.StartGame(playerNames);

            //while(bowlingManager.GameStarted)
            //{
            //    //Random de 10 sau rest
            //    var num = new Random();
            //    bowlingManager.NextShot(num.Next(10));
            //}
            //bowlingManager.GetStanding();

            //startGame
            //shot

            //c.writeInConsole("Standing: ");

            
        }
    }
}
