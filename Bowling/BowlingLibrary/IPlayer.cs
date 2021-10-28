using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLibrary
{
    public interface IPlayer
    {
        string Name { get; }
        int? TotalScore { get; }
    }
}
