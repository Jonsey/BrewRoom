using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Models;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec.Models
{
    [TestFixture]
    public class RecipeHopSpecs
    {
        [Test]
        public void ShouldUpdateUtilizationWhenTheBoilTimeIsChanged()
        {
            var recipe = new Recipe();
            var recipeFermentable = new RecipeFermentable(recipe, new StockFermentable("Pils Malt", 1.045M), 5.KiloGrams());
            var recipeHop = new RecipeHop(new Hop("Saaz"), 10.Grams(), 60, 10M, recipe); 
            
            Assert.AreEqual(0.4, recipeHop.Utilization);

            recipeHop.BoilTime = 20;

            Assert.AreEqual(0.2, recipeHop.Utilization);
        }
    }
}
