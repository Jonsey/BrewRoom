using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;

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
            editRecipeVm.SelectedStockFermentable = grainVMs[0];

            editRecipeVm.AddFermentableCommand.Execute();

            Assert.AreEqual(grainVMs[0].Name, editRecipeVm.RecipeFermentables[0].Name);
        }

        [Test]
        public void ShouldBeAbleToremoveTheSelectedFermentableFromTheRecipe()
        {
            var stockItemsViewModel = MockRepository.GenerateMock<IStockItemsViewModel>();
            var editRecipeVm = new EditRecipeViewModel(eventAggregator, stockItemsViewModel);
            
            editRecipeVm.SelectedStockFermentable = grainVMs[0];
            editRecipeVm.AddFermentableCommand.Execute();

            Assert.AreEqual(1, editRecipeVm.RecipeFermentables.Count);

            editRecipeVm.SelectedRecipeFermentable = editRecipeVm.RecipeFermentables[0];

            editRecipeVm.RemoveFermentableCommand.Execute();

            Assert.AreEqual(0, editRecipeVm.RecipeFermentables.Count);
        }
    }
}
