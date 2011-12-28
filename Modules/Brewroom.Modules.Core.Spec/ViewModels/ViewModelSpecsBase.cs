using System.Collections.Generic;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Models;
using BrewRoom.Modules.Core.ViewModels;
using Microsoft.Practices.Prism.Events;
using NUnit.Framework;
using Rhino.Mocks;

namespace Brewroom.Modules.Core.Spec.ViewModels
{
    public class ViewModelSpecsBase
    {
        protected IEventAggregator eventAggregator;
        protected IStockItemsRepository stockItemsRepository;
        protected List<IStockFermentableViewModel> grainVMs;
        protected List<IStockHopViewModel> hopVMs;

        void SetupFermentables()
        {
            var grain1 = new StockFermentable("Pils Malt", 1.045M);
            var grain2 = new StockFermentable("Amber Malt", 1.040M);

            var grains = new List<IStockFermentable>
                             {
                                 grain1,
                                 grain2
                             };

            grainVMs = new List<IStockFermentableViewModel>
                           {
                               new StockFermentableViewModel(grains[0]),
                               new StockFermentableViewModel(grains[1])
                           };

            stockItemsRepository.Expect(x => x.GetGrains()).Return(grains).Repeat.Any();
        }

        void SetupHops()
        {
            IHop hop = new Hop("Saaz");
            hop.AddOilCharacteristics(new HopOilCharacteristics
            {
                Carophyllene = 20M,
                Farnesene = 20M,
                Humulene = 20M,
                Myrcene = 20M,
                OtherAcids = 20M,
                PercentageOfTotalWeight = 20,
                TotalAlphaAcid = 5M
            });

            IList<IHop> hops = new List<IHop>
                        {
                            hop
                        };

            hopVMs = new List<IStockHopViewModel>
                           {
                               new StockHopViewModel(hops[0])
                           };

            stockItemsRepository.Expect(x => x.GetHops()).Return(hops).Repeat.Any();
        }

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            eventAggregator = new EventAggregator();

            
        }

        [SetUp]
        public void Init()
        {
            stockItemsRepository = MockRepository.GenerateMock<IStockItemsRepository>();

            SetupFermentables();
            SetupHops();
        }
    }
}