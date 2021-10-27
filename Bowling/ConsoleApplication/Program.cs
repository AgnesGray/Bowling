using BowlingLibrary;
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BowlingManager bowlingManager = new BowlingManager();
            ConsoleApplication c = new ConsoleApplication();

            /*read players
            c.writeInConsole("number of players");
            c.readConsole();

            //read names
            c.writeInConsole("players names");
            c.readConsole();

            //read frames
            c.writeInConsole("number of frames");
            c.readConsole();*/


            //get nr of frames
            bowlingManager.framesNumber = 3;

            //get players
            var playerNames = new List<string>();
            
                playerNames.Add("A");
                playerNames.Add("B");
                playerNames.Add("C");


            
            bowlingManager.StartGame(playerNames);

            while(bowlingManager.GameStarted)
            {
                //Random de 10 sau rest
                var num = new Random();
                bowlingManager.NextShot(num.Next(10));
            }
            bowlingManager.GetStanding();

            //startGame
            //shot

            c.writeInConsole("Standing: ");

            
        }
    }
}
