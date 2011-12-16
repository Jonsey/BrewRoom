using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class HopMap : ClassMap<Hop>
    {
        public HopMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            Component<IHopOilCharacteristics>(x => x.Characteristics);
        }
    }
}
