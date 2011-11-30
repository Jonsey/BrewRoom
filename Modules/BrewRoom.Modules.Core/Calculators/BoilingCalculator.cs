using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Calculators
{
    public class BoilingCalculator
    {
        private readonly decimal _energy;

        public BoilingCalculator(decimal energy)
        {
            _energy = energy;
        }

        public decimal GetTimeToTemperature(decimal fromTemp, decimal toTemp, Volume volume)
        {
            return 20M;
        }
    }
}
