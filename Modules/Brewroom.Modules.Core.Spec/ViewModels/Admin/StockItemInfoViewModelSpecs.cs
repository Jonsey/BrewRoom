using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.ViewModels.Admin;
using NUnit.Framework;
using Rhino.Mocks;

namespace Brewroom.Modules.Core.Spec.ViewModels.Admin
{
    [TestFixture]
    public class StockItemInfoViewModelSpecs
    {
        [Test]
        public void ShouldBeAbleToLoadBYOHopPages()
        {
            var vm = new StockItemInfoViewModel(MockRepository.GenerateMock<IStockItemsRepository>());
            vm.GetHopsCommand.Execute();
        }
    }
}
