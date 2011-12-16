using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class RecipeFermentableMap : ClassMap<RecipeFermentable>
    {
        public RecipeFermentableMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Pppg);
            Component(x => x.Weight);

            References(x => x.Recipe);
        }
    }
}
