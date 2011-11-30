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
            var grain = new Grain("Pils Malt");
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
            var grain1 = new Grain("Pils Malt 1");
            var grain2 = new Grain("Pils Malt 2");

            var recipe = new Recipe();
            recipe.AddGrain(grain1, 1.KiloGram());
            recipe.AddGrain(grain2, 2.KiloGrams());

            Assert.AreEqual(3.KiloGrams(), recipe.GetTotalGrainWeight());
        }

        [Test]
        public void ShouldCalculateStartingGravity()
        {
            var grain1 = new Grain("Wheat");

            var recipe = new Recipe();
            recipe.SetBrewLength(new Volume(1, VolumeUnit.Gallons));
            recipe.AddGrain(grain1, new Weight(1, MassUnit.Pounds), 1.045M);

            Assert.AreEqual(1.045M, recipe.GetStartingGravity());
        }

        [Test]
        public void ShouldCalculateStartingGravityWithMultipleGrains()
        {
            var grain1 = new Grain("Wheat");
            var grain2 = new Grain("Honey");
            var grain3 = new Grain("Two-row");

            var recipe = new Recipe();
            recipe.SetBrewLength(new Volume(5.5M, VolumeUnit.Gallons));
            recipe.AddGrain(grain1, new Weight(1.87M, MassUnit.Pounds), 1.038M);
            recipe.AddGrain(grain2, new Weight(1.46M, MassUnit.Pounds), 1.033M);
            recipe.AddGrain(grain3, new Weight(5.92M, MassUnit.Pounds), 1.036M);

            Assert.AreEqual(1.049M, recipe.GetStartingGravity());
        }

        [Test]
        public void ShouldBeAbleToClaculateHopUtilization()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ShouldBeAbleToClaculateTotalIBU()
        {
            var recipe = SimpleRecipe();

            Assert.AreEqual(31.2M, recipe.GetIBU());
        }

        private static Recipe SimpleRecipe()
        {
            var grain1 = new Grain("Wheat");
            var hop = new Hop("Saaz");
            var recipe = new Recipe();

            recipe.SetBrewLength(6.Gallons());
            recipe.AddGrain(grain1, 1.Pound(), 1.045M);
            recipe.AddHop(hop, new Weight(1/16M, MassUnit.Pounds), 60, 12.5M);
            return recipe;
        }

        [Test]
        public void ShouldCalculateBuGuRatio()
        {
            var recipe = SimpleRecipe();

            Assert.AreEqual(0.81M, recipe.GetGuBuRatio());
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
