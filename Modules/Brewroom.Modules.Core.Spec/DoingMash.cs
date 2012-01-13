using System;
using BrewRoom.Modules.Core.Calculators;
using BrewRoom.Modules.Core.Models;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec
{
    [TestFixture]
    public class DoingMash : RecipeBase
    {
        [Test]
        public void ShouldCalculateStrikeWaterTemperature()
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
        public void ShouldCalculateMyStrikeWaterTemperature()
        {
            var mashCalc = new MashCalculator();

            var desiredStrikeTemp = 140;
            var grainTemp = 60;
            var ratio = 1M;

            var infusionWaterTemp = mashCalc.GetStrikeTemp(desiredStrikeTemp, grainTemp, ratio, 8.KiloGrams());

            Assert.AreEqual(156M, infusionWaterTemp);
        }

        [Test]
        public void ShouldCalculateMyNextInfusionStep()
        {
            var desiredStrikeTemp = 140;
            var grainTemp = 60;
            var ratio = 1;

            var mashCalc = new MashCalculator();

            var strikeTemp = mashCalc.GetStrikeTemp(desiredStrikeTemp, grainTemp, ratio, 8.KiloGrams());

            var grain = new Weight(8, MassUnit.KiloGrams);
            var desiredInfusionTemp = 154M;
            var liquorTemperature = 210M;

            var amountOfWaterToAdd = mashCalc.GetInfusionAmount(ratio, grain, desiredInfusionTemp, liquorTemperature,
                                                                desiredStrikeTemp);

            Assert.AreEqual(5.0M, amountOfWaterToAdd);
        }

        [Test]
        public void ShouldCalculatenextInfusionStep()
        {
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
        public void RecipeGrainShouldCalculateGravityContribution()
        {
            var recipe = CreateDefaultRecipe();
            var fermentables = recipe.Fermentables;
            var gravityContribution = fermentables[0].GravityContribution;

            Assert.AreEqual(1.094M, gravityContribution);
        }

        [Test]
        public void RecipeGrainShouldTakePppgOfSelectedGrain()
        {
            var grain = new StockFermentable("Pils Malt", 1.045M);
            var recipe = new Recipe();

            recipe.AddFermentable(grain, 1.KiloGram());

            Assert.AreEqual(1.045M, recipe.Fermentables[0].Pppg);
        }

        [Test]
        public void SingleRecipeGrainShouldBe100PercentOfMash()
        {
            var grain = new StockFermentable("Pils Malt", 1.045M);
            var recipe = new Recipe();

            recipe.AddFermentable(grain, 1.KiloGram());

            Assert.AreEqual(100M, recipe.Fermentables[0].PercentageOfMash);
        }
    }
}
