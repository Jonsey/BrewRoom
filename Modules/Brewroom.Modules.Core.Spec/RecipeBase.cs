﻿using BrewRoom.Modules.Core.Models;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec
{
    public class RecipeBase
    {
        public Recipe CreateDefaultRecipe()
        {
            var grain = new Grain("Two-row");
            var hop = new Hop("Saaz");
            var recipe = new Recipe();

            recipe.SetBrewLength(new Volume(20, VolumeUnit.Litres));
            recipe.AddGrain(grain, 5.KiloGrams(), 1.045M);
            recipe.AddHop(hop, new Weight(200M, MassUnit.Grams), 60, 12.5M);

            return recipe;
        }
    }
}