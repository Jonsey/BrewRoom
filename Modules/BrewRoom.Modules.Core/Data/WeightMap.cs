using FluentNHibernate.Mapping;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Data
{
    public class WeightMap : ComponentMap<Weight>
    {
        public WeightMap()
        {
            Map(x => x.Value).Column("Amount");
            Map(x => x.Unit).Column("Unit");
        }
    }
}
