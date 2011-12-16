using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Data
{
    public class VolumeMap : ComponentMap<Volume>
    {
        public VolumeMap()
        {
            Map(x => x.Value).Column("VolumeValue");
            Map(x => x.Unit).Column("VolumeUnit");
        }
    }
}
