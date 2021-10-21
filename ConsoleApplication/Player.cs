using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApplication
{
    public class Player : IPlayer
    {
        public string Name { get; }
        public int TotalScore { get; }

        public Player(string Name, int TotalScore)
        {
            this.Name = Name;
            this.TotalScore = TotalScore;
        }
    }
}
