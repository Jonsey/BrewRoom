using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Calculators;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec
{
    [TestFixture]
    public class BoilingCalculationTests
    {
        [Test]
        public void ShouldCalculateTimeTakenToBoilAmountOfWater()
        {
            var energy = 8.5M;
            var calc = new BoilingCalculator(energy);
            var fromTemp = 0M;
            var toTemp = 212M;
            var volume = 1.Litres();

            var timeToTemp = calc.GetTimeToTemperature(fromTemp, toTemp, volume);

            Assert.AreEqual(8.5M, timeToTemp);
        }
    }
}
