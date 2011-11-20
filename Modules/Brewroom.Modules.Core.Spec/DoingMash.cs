using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core;
using BrewRoom.Modules.Core.ValueObjects;
using NUnit.Framework;

namespace Brewroom.Modules.Core.Spec
{
    [TestFixture]
    public class DoingMash : RecipeBase
    {
        [Test]
        public void ShouldCalculateStrikeTemperature()
        {
            var mashCalc = new MashCalculator();

            var recipe = CreateDefaultRecipe();

            var desiredStrikeTemp = 104;
            var grainTemp = 70;
            var ratio = 1;

            var strikeTemp = mashCalc.GetStrikeTemp(desiredStrikeTemp, grainTemp, ratio, 8.Pounds());

            Assert.AreEqual(110.8M, strikeTemp);
        }

        [Test]
        public void ShouldCalculatenextInfusionStep()
        {
            var recipe = CreateDefaultRecipe();

            var desiredStrikeTemp = 104;
            var grainTemp = 70;
            var ratio = 1;

            var mashCalc = new MashCalculator();

            var strikeTemp = mashCalc.GetStrikeTemp(desiredStrikeTemp, grainTemp, ratio, 8.Pounds());

            var grain = new Weight(8, MassUnit.Pounds);
            var desiredInfusionTemp = 140M;
            var liquorTemperature = 210M;

            var amountOfWaterToAdd = mashCalc.GetInfusionAmount(ratio, grain, desiredInfusionTemp, liquorTemperature,
                                                                strikeTemp);

            Assert.AreEqual(4.0M, amountOfWaterToAdd);
        }

        [Test]
        public void shouldSetTheRatioWhenConstructed()
        {


        }
    }

    public class MashCalculator
    {
        private decimal _grainAmount;
        private decimal _liquorAmount;
        private decimal _ratio;

        public MashCalculator()
        {

        }

        public MashCalculator(decimal grainAmount, decimal liquorAmount)
        {
            _grainAmount = grainAmount;
            _liquorAmount = liquorAmount;
            _ratio = CalculateRatio();
            ;
        }

        private decimal CalculateRatio()
        {
            return _grainAmount / _liquorAmount;
        }

        public decimal GetStrikeTemp(decimal desiredStrikeTemp, decimal grainTemp, decimal ratio, Weight grainAmount)
        {
            var tw = (0.2M / ratio) * (desiredStrikeTemp - grainTemp) + desiredStrikeTemp;

            _liquorAmount = (grainAmount * tw).ConvertTo(MassUnit.KiloGrams).GetValue();
            _grainAmount = grainAmount.ConvertTo(MassUnit.KiloGrams).GetValue();

            return tw;
        }

        public decimal GetInfusionAmount(Weight grain, decimal desiredInfusionTemp, decimal liquorTemperature, decimal strikeTemp)
        {
            ReCalcRatio();

            decimal grainWeight = grain.ConvertTo(MassUnit.Pounds).GetValue();
            var wm = _ratio * grainWeight;


            var wa = (desiredInfusionTemp - strikeTemp) * ((0.2M * grainWeight) + wm) /
                     (liquorTemperature - desiredInfusionTemp);

            _liquorAmount += wa;

            ReCalcRatio();

            return Math.Round(wa);
        }

        public decimal GetInfusionAmount(decimal ratio, Weight grain, decimal desiredInfusionTemp, decimal liquorTemperature, decimal strikeTemp)
        {
            decimal grainWeight = grain.ConvertTo(MassUnit.Pounds).GetValue();
            var wm = ratio * grainWeight;


            var wa = (desiredInfusionTemp - strikeTemp) * ((0.2M * grainWeight) + wm) /
                     (liquorTemperature - desiredInfusionTemp);

            return Math.Round(wa);
        }

        private void ReCalcRatio()
        {
            _ratio = _grainAmount / _liquorAmount;
        }
    }
}
