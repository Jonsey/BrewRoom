using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.ViewModels;
using NUnit.Framework;

namespace Brewroom.Modules.Core.Spec.ViewModels
{
    [TestFixture]
    public class StockItemsSpecs : ViewModelSpecsBase
    {
        [Test]
        public void ShouldListFermentables()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            Assert.AreEqual(grainVMs, vm.Fermentables);
        }

        [Test]
        public void ShouldSelectFermentableDetails()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedStockItem = grainVMs[0];

            Assert.AreEqual(grainVMs[0], vm.SelectedStockItem);
        }

        [Test]
        public void ShouldUnSelectAnyStockItemsWhenChangingTabs()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedStockItem = grainVMs[0];
            Assert.IsNotNull(vm.SelectedStockItem, "Stock item was not selected.");

            vm.SelectHops.Execute();

            Assert.IsNull(vm.SelectedStockItem, "Stock item was not de-selected.");
        }

        [Test]
        public void ShouldExposeSelectedFermentableViewModel()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedStockItem = grainVMs[0];

            Assert.IsInstanceOf<IFermentableViewModel>(vm.SelectedStockItem);
        }

        [Test]
        public void ShouldListHops()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            Assert.IsNotNull(vm.Hops, "View model hops is null.");
            Assert.AreEqual(hopVMs, vm.Hops);
        }

        [Test]
        public void ShouldShowHopDetailsWhenHopsAreSelected()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectHops.Execute();

            Assert.IsTrue(vm.IsHopDetailsVisible);
        }

        [Test]
        public void ShouldShowFermentableDetailsWhenFermentablesAreSelected()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectFermentables.Execute();

            Assert.IsTrue(vm.IsFermentableDetailsVisible);
        }

        [Test]
        public void ShouldExposeSelectedHopName()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedStockItem = hopVMs[0];

            Assert.AreEqual("Saaz", vm.SelectedStockItem.Name);
        }

        [Test]
        public void ShouldExposeSelectedHopAlphaAcid()
        {
            IStockItemsViewModel vm = new StockItemsViewModel(eventAggregator, stockItemsRepository);

            vm.SelectedStockItem = hopVMs[0];

            var selectHop = vm.SelectedStockItem as IHopViewModel;
            Assert.AreEqual(5M, selectHop.AlphaAcid);
        }
    }
}
