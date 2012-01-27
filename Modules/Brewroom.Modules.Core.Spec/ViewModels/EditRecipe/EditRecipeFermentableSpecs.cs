using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.ViewModels;
using NUnit.Framework;
using Zymurgy.Dymensions;

namespace Brewroom.Modules.Core.Spec.ViewModels.EditRecipe
{
    [TestFixture]
    public class EditRecipeFermentableSpecs : ViewModelSpecsBase
    {
        private IStockItemsViewModel _stockItemsVm;
        private EditRecipeViewModel _editRecipeVm;

        [Test]
        public void ShouldUpdateFermentableWeight()
        {
            _stockItemsVm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            _editRecipeVm = new EditRecipeViewModel(eventAggregator, _stockItemsVm, _recipeRepository);

            _stockItemsVm.SelectedFermentable = grainVMs[0];
            _stockItemsVm.AddFermentableToRecipeCommand.Execute();

            _editRecipeVm.SelectedFermentable = _editRecipeVm.RecipeFermentables[0];
            _editRecipeVm.SelectedFermentable.Weight = 5.KiloGrams();

            Assert.AreEqual(5.KiloGrams(), _editRecipeVm.SelectedFermentable.Weight);
        }
    }
}
