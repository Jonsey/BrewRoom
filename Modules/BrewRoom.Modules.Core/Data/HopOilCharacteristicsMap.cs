using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class HopOilCharacteristicsMap : ComponentMap<IHopOilCharacteristics>
    {
        public HopOilCharacteristicsMap()
        {
            Map(x => x.Carophyllene);
            Map(x => x.Farnesene);
            Map(x => x.Humulene);
            Map(x => x.Myrcene);
            Map(x => x.OtherAcids);
            Map(x => x.PercentageOfTotalWeight);
            Map(x => x.TotalAlphaAcid);

        }
    }
}
