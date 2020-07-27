using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IRoulette
    {
        string Bet(string id, Bet bet, double maxAmount, string[] rangeNumbersBet, string[] allowedColors);
    }
}
