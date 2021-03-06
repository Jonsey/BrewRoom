﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class RecipeHopMap : ClassMap<RecipeHop>
    {
        public RecipeHopMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description).CustomSqlType("nvarchar(MAX)");

            Component<HopOilCharacteristics>(x => x.Characteristics, m =>
                                                                         {
                                                                             m.Map(x => x.Carophyllene);
                                                                             m.Map(x => x.Farnesene);
                                                                             m.Map(x => x.Humulene);
                                                                             m.Map(x => x.Myrcene);
                                                                             m.Map(x => x.OtherAcids);
                                                                             m.Map(x => x.PercentageOfTotalWeight);
                                                                             m.Map(x => x.TotalAlphaAcid);
                                                                         }

                );

            Map(x => x.BoilTime);

            Component(x => x.Weight);

            References(x => x.Recipe);
        }
    }
}
