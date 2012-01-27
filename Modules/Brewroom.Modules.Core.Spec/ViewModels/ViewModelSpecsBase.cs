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
        protected IStockItemsRepository _stockItemsRepository;
        protected IRecipeRepository _recipeRepository;
        protected IList<IFermentableViewModel> grainVMs;
        protected IList<IHopViewModel> hopVMs;

        void SetupFermentables()
        {
            var grain1 = new StockFermentable("Pils Malt", 1.045M);
            var grain2 = new StockFermentable("Amber Malt", 1.040M);

            var grains = new List<IFermentable>
                             {
                                 grain1,
                                 grain2
                             };

            grainVMs = new List<IFermentableViewModel>
                           {
                               new FermentableViewModel(grains[0]),
                               new FermentableViewModel(grains[1])
                           };

            _stockItemsRepository.Expect(x => x.GetStockFermentables()).Return(grains).Repeat.Any();
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

            hopVMs = new List<IHopViewModel>
                           {
                               new HopViewModel(hops[0])
                           };

            _stockItemsRepository.Expect(x => x.GetHops()).Return(hops).Repeat.Any();
        }

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            eventAggregator = new EventAggregator();  
        }

        [SetUp]
        public void Init()
        {
            _stockItemsRepository = MockRepository.GenerateMock<IStockItemsRepository>();
            _recipeRepository = MockRepository.GenerateMock<IRecipeRepository>();

            SetupFermentables();
            SetupHops();
        }
    }
}