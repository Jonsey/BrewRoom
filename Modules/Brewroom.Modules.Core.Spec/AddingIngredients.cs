using System;
using BrewRoom.Modules.Core.Models;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec
{
    [TestFixture]
    public class AddingIngredients
    {
        [Test]
        public void GrainsShouldAlwaysHaveAName()
        {
            var grain = new Fermentable("Pils Malt");
            Assert.AreEqual("Pils Malt", grain.Name);
        }

        [Test]
        public void HopsShouldAlwaysHaveAName()
        {
            var hop = new Hop("Saaz");
            Assert.AreEqual("Saaz", hop.Name);
        }

        [Test]
        public void ShouldBeAbleToSetTheWeightOfHops()
        {
            var hop = new Hop("Saaz");

            var recipe = new Recipe();
            recipe.AddHop(hop, new Weight(10, MassUnit.Grams), 60);

            Assert.AreEqual(new Weight(10, MassUnit.Grams), recipe.GetTotalHopWeight());
        }

        [Test]
        public void ShouldHandleGramsAndKilograms()
        {
            var hop1 = new Hop("Saaz 1");
            var hop2 = new Hop("Saaz 2");

            var recipe = new Recipe();
            recipe.AddHop(hop1, new Weight(10, MassUnit.Grams), 60);
            recipe.AddHop(hop2, new Weight(1, MassUnit.KiloGrams), 60);

            Assert.AreEqual(new Weight(1010, MassUnit.Grams), recipe.GetTotalHopWeight());
        }

        [Test]
        public void ShouldBeAbleToAddGrains()
        {
            var grain1 = new Fermentable("Pils Malt 1");
            var grain2 = new Fermentable("Pils Malt 2");

            var recipe = new Recipe();
            recipe.AddFermentable(grain1, 1.KiloGram());
            recipe.AddFermentable(grain2, 2.KiloGrams());

            Assert.AreEqual(3.KiloGrams(), recipe.GetTotalGrainWeight());
        }

        [Test]
        public void ShouldCalculateStartingGravity()
        {
            var grain1 = new Fermentable("Wheat");

            var recipe = new Recipe();
            recipe.SetBrewLength(new Volume(1, VolumeUnit.Gallons));
            recipe.AddFermentable(grain1, new Weight(1, MassUnit.Pounds), 1.045M);

            Assert.AreEqual(1.045M, recipe.GetStartingGravity());
        }

        [Test]
        public void ShouldCalculateStartingGravityWithMultipleGrains()
        {
            var grain1 = new Fermentable("Wheat");
            var grain2 = new Fermentable("Honey");
            var grain3 = new Fermentable("Two-row");

            var recipe = new Recipe();
            recipe.SetBrewLength(new Volume(3M, VolumeUnit.Gallons));
            recipe.AddFermentable(grain1, new Weight(1M, MassUnit.Pounds), 1.045M);
            recipe.AddFermentable(grain2, new Weight(1M, MassUnit.Pounds), 1.045M);
            recipe.AddFermentable(grain3, new Weight(1M, MassUnit.Pounds), 1.046M);

            Assert.AreEqual(1.045M, recipe.GetStartingGravity());
        }

        [Test]
        public void ShouldBeAbleToClaculateHopUtilization()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ShouldBeAbleToClaculateTotalIbu()
        {
            var recipe = SimpleRecipe();

            Assert.AreEqual(66M, recipe.GetIbu());
        }

        private static Recipe SimpleRecipe()
        {
            var grain1 = new Fermentable("Wheat");
            var hop = new Hop("Saaz");
            var recipe = new Recipe();

            recipe.SetBrewLength(1.Gallons());
            recipe.AddFermentable(grain1, 1.Pound(), 1.045M);
            recipe.AddHop(hop, new Weight(10M, MassUnit.Grams), 60, 12.5M);
            return recipe;
        }

        [Test]
        public void ShouldCalculateBuGuRatio()
        {
            var recipe = SimpleRecipe();

            Assert.AreEqual(1.47M, recipe.GetBuGuRatio());
        }

        [Test]
        public void ShouldCalculateBuGuRatioAsZeroIfNoHops()
        {
            var grain1 = new Fermentable("Wheat");
            var recipe = new Recipe();

            recipe.SetBrewLength(1.Gallons());
            recipe.AddFermentable(grain1, 1.Pound(), 1.045M);

            Assert.AreEqual(0M, recipe.GetBuGuRatio());
        }

        [Test]
        public void ShouldSetBrewLength()
        {
            var recipe = new Recipe();
            recipe.SetBrewLength(new Volume(100, VolumeUnit.Litres));

            Assert.AreEqual(new Volume(100, VolumeUnit.Litres), recipe.GetBrewLength());
        }

        [Test]
        public void ShouldBeAbleToAddHopOilCharacteristics()
        {
            var hop = new Hop("Saaz");
            var hopOilCharacteristics = new HopOilCharacteristics()
                                            {
                                                PercentageOfTotalWeight = 10,
                                                Farnesene = 20,
                                                Carophyllene = 30,
                                                Myrcene = 15,
                                                Humulene = 15,
                                                OtherAcids = 20
                                            };

            hop.AddOilCharacteristics(hopOilCharacteristics);

            Assert.AreEqual(hopOilCharacteristics, hop.GetCharacteristics());
        }

        [Test]
        public void ShouldThrowAnExceptionIfOilPercentagesDoNotAddUp()
        {
            var hop = new Hop("Saaz");
            var hopOilCharacteristics = new HopOilCharacteristics()
            {
                PercentageOfTotalWeight = 10,
                Farnesene = 20,
                Carophyllene = 20,
                Myrcene = 15,
                Humulene = 15,
                OtherAcids = 20
            };

            try
            {
                hop.AddOilCharacteristics(hopOilCharacteristics);
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail("Hop oils failed validation.");
        }
    }
}
