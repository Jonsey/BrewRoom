using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Models;

namespace BrewRoom.Modules.Core.Repositories
{
    public class StockItemsRepository : IStockItemsRepository
    {
        public IEnumerable<IFermentable> GetGrains()
        {
            var fermentables = new List<IFermentable>();

            var fermentable1 = new Fermentable("Marris Otter", 1.045M);
            var fermentable2 = new Fermentable("Pils Malt", 1.038M);

            fermentables.Add(fermentable1);
            fermentables.Add(fermentable2);

            return fermentables;
        }

        public IEnumerable<IHop> GetHops()
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

            var hops = new List<IHop>
                        {
                            hop
                        };

            return hops;
        }
    }
}
