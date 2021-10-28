using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public class Player : IPlayer
    {
        public string Name { get; set; }

        public int? TotalScore { get; set; }

        public Player(string Name, int? TotalScore)
        {
            this.Name = Name;
            this.TotalScore = TotalScore;
        }
    }
}
