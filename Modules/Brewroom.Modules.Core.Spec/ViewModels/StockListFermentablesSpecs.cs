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

            Assert.AreEqual(1.045, vm.SelectedFermentable.Pppg);
        } 

        [Test]
        public void ShouldPublishFermentableSelectedEvent()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);
            IEditRecipeViewModel recipeVm = new EditRecipeViewModel(eventAggregator, vm);

            vm.SelectedFermentable = grainVMs[0];

            Assert.AreEqual(grainVMs[0], recipeVm.SelectedStockFermentable);
        }
        #endregion
    }
}
