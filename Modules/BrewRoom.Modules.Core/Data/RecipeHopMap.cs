using System;
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

            Component(x => x.Weight);

            References(x => x.Recipe);
        }
    }
}
