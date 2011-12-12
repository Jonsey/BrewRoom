using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec.ViewModels
{
    [TestFixture]
    public class EditRecipeSpecs : ViewModelSpecsBase
    {
        [Test]
        public void ShouldBeAbleToAddTheSelectedFermentableToTheRecipe()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);

            editRecipeVm.SelectedStockItem = grainVMs[0];

            editRecipeVm.AddSelectedStockItemCommand.Execute();

            Assert.AreEqual(grainVMs[0].Name, editRecipeVm.RecipeFermentables[0].Name);
        }

        [Test]
        public void ShouldBeAbleToAddTheSelectedHopToTheRecipe()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);
            editRecipeVm.SelectedStockItem = hopVMs[0];

            editRecipeVm.AddSelectedStockItemCommand.Execute();

            Assert.AreEqual(hopVMs[0].Name, editRecipeVm.RecipeHops[0].Name);
        }

        [Test]
        public void ShouldBeAbleToremoveTheSelectedFermentableFromTheRecipe()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);
            
            editRecipeVm.SelectedStockItem = grainVMs[0];
            editRecipeVm.AddSelectedStockItemCommand.Execute();

            Assert.AreEqual(1, editRecipeVm.RecipeFermentables.Count);

            editRecipeVm.SelectedRecipeFermentable = editRecipeVm.RecipeFermentables[0];

            editRecipeVm.RemoveFermentableCommand.Execute();

            Assert.AreEqual(0, editRecipeVm.RecipeFermentables.Count);
        }

        [Test]
        public void ShouldAggregateFermentablesWhenAdded()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);

            editRecipeVm.SelectedStockItem = grainVMs[0];
            editRecipeVm.AddSelectedStockItemCommand.Execute();
            editRecipeVm.AddSelectedStockItemCommand.Execute();

            Assert.AreEqual(1, editRecipeVm.RecipeFermentables.Count);
            Assert.AreEqual(2.KiloGrams(), editRecipeVm.RecipeTotalGrainWeight);
        }

        [Test]
        public void ShouldNotAggregateFermentablesWhenAddedWithAlteredPppg()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);

            editRecipeVm.SelectedStockItem = grainVMs[0];
            editRecipeVm.AddSelectedStockItemCommand.Execute();
            grainVMs[0].Pppg = 1.010M;
            editRecipeVm.AddSelectedStockItemCommand.Execute();

            Assert.AreEqual(2, editRecipeVm.RecipeFermentables.Count);
            Assert.AreEqual(2.KiloGrams(), editRecipeVm.RecipeTotalGrainWeight);
        }


    }
}
