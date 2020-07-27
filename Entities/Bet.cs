using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Bet
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Bettor { get; set; }

        public bool ValidateNumber(int[] range, int betNumber)
        {
            try
            {
                int min = range.Min();
                int max = range.Max();
                return min <= betNumber && betNumber <= max;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool ValidateMaxAmount(double maxAmount) => maxAmount >= this.Amount;
    }
}