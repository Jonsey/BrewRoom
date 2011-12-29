using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using NHibernate;

namespace BrewRoom.Modules.Core.Repositories
{
    public class RecipeRepository : Repository, IRecipeRepository
    {
        public RecipeRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }
        public void Save(IRecipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
