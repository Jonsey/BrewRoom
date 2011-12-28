using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Events;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Models;
using BrewRoom.Modules.Core.ViewModels;
using Microsoft.Practices.Prism.Events;
using NUnit.Framework;
using Rhino.Mocks;

namespace Brewroom.Modules.Core.Spec.ViewModels
{
    [TestFixture]
    public class StockListFermentablesSpecs : ViewModelSpecsBase
    {
        #region FermenatbleViewModel Specs
        [Test]
        public void ShouldExposeSelectedFermentableName()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedFermentable = grainVMs[1];

            Assert.AreEqual("Amber Malt", vm.SelectedFermentable.Name);
        }

        [Test]
        public void ShouldUpdateSelectedFermentableName()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedFermentable = grainVMs[1];
            vm.SelectedFermentable.Name = "Amber Malt changed";

            Assert.AreEqual("Amber Malt changed", grainVMs[1].Name);
        }

        [Test]
        public void ShouldExposeSelectedFermentablePppg()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedFermentable = grainVMs[0];

            var selectedFermentable = vm.SelectedFermentable;
            Assert.AreEqual(1.045, selectedFermentable.Pppg);
        } 

        [Test]
        public void RecipeViewModelShouldPickUpSelectedStockFermentable()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);
            IEditRecipeViewModel recipeVm = new EditRecipeViewModel(eventAggregator, vm);

            vm.SelectedFermentable = grainVMs[0];

            Assert.AreEqual(grainVMs[0], recipeVm.SelectedStockItem);
        }

        [Test]
        public void RecipeViewModelShouldPickUpSelectedStockHop()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);
            IEditRecipeViewModel recipeVm = new EditRecipeViewModel(eventAggregator, vm);

            vm.SelectedHop = hopVMs[0];

            Assert.AreEqual(hopVMs[0], recipeVm.SelectedStockItem);
        }
        #endregion

        #region Save Fermentables

        [Test]
        public void ShouldSaveFermentable()
        {
            var id = Guid.NewGuid();
            stockItemsRepository.Expect(x => x.Save(Arg<Fermentable>.Is.Anything)).Return(id);
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedFermentable = grainVMs[0];
            grainVMs[0].Name = "Changed name";

            vm.SaveFermentableCommand.Execute();

            stockItemsRepository.VerifyAllExpectations();
        }


        #endregion
    }
}
