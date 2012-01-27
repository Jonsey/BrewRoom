using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class StockFermentableMap : ClassMap<StockFermentable>
    {
        public StockFermentableMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            
            Map(x => x.Name);
            Map(x => x.Pppg);
            Map(x => x.Description).CustomSqlType("nvarchar(MAX)");
        }
    }
}