﻿using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.ViewModels;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec.ViewModels.EditRecipe
{
    [TestFixture]
    public class EditRecipeSpecs : ViewModelSpecsBase
    {
        [Test]
        public void ShouldBeAbleToremoveTheSelectedFermentableFromTheRecipe()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, vm, _recipeRepository);

            vm.SelectedFermentable = grainVMs[0];
            vm.AddFermentableToRecipeCommand.Execute();

            Assert.AreEqual(1, editRecipeVm.RecipeFermentables.Count);

            editRecipeVm.SelectedFermentable = editRecipeVm.RecipeFermentables[0];

            editRecipeVm.RemoveFermentableCommand.Execute();

            Assert.AreEqual(0, editRecipeVm.RecipeFermentables.Count);
        }

        [Test]
        public void ShouldAggregateFermentablesWhenAdded()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, vm, _recipeRepository);

            vm.SelectedFermentable = grainVMs[0];
            vm.AddFermentableToRecipeCommand.Execute();
            vm.AddFermentableToRecipeCommand.Execute();

            Assert.AreEqual(1, editRecipeVm.RecipeFermentables.Count);
            Assert.AreEqual(2.KiloGrams(), editRecipeVm.RecipeTotalGrainWeight);
        }

        [Test]
        public void ShouldNotAggregateFermentablesWhenAddedWithAlteredPppg()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, vm, _recipeRepository);

            vm.SelectedFermentable = grainVMs[0];
            vm.AddFermentableToRecipeCommand.Execute();
            vm.SelectedFermentable.Pppg = 1.010M;
            vm.AddFermentableToRecipeCommand.Execute();

            Assert.AreEqual(2, editRecipeVm.RecipeFermentables.Count);
            Assert.AreEqual(2.KiloGrams(), editRecipeVm.RecipeTotalGrainWeight);
        }


    }
}
