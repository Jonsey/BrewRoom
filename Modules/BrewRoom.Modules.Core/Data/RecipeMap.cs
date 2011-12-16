using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public class RecipeMap : ClassMap<Recipe>
    {
        public RecipeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            HasMany<RecipeHop>(x => x.Hops);
            HasMany<RecipeFermentable>(x => x.Fermentables);
        }
    }
}
