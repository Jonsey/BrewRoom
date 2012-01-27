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
    public class EditRecipeHopSpecs : ViewModelSpecsBase
    {
        private IStockItemsViewModel _stockItemsVm;
        private EditRecipeViewModel _editRecipeVm;

        [Test]
        public void ShouldUpdateHopWeight()
        {
            _stockItemsVm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            _editRecipeVm = new EditRecipeViewModel(eventAggregator, _stockItemsVm, _recipeRepository);

            _stockItemsVm.SelectedHop = hopVMs[0];
            _stockItemsVm.AddHopToRecipeCommand.Execute();

            _editRecipeVm.SelectedHop = _editRecipeVm.RecipeHops[0];
            _editRecipeVm.SelectedHop.Weight = 5.KiloGrams();

            Assert.AreEqual(5.KiloGrams(), _editRecipeVm.SelectedHop.Weight);
        }

        [Test]
        public void ShouldUpdateHopBoilTime()
        {
            _stockItemsVm = new StockItemsViewModel(eventAggregator, _stockItemsRepository);
            _editRecipeVm = new EditRecipeViewModel(eventAggregator, _stockItemsVm, _recipeRepository);

            _stockItemsVm.SelectedHop = hopVMs[0];
            _stockItemsVm.AddHopToRecipeCommand.Execute();

            _editRecipeVm.SelectedHop = _editRecipeVm.RecipeHops[0];
            _editRecipeVm.SelectedHop.BoilTime = 20;

            Assert.AreEqual(20, _editRecipeVm.SelectedHop.BoilTime);
        }
    }
}
