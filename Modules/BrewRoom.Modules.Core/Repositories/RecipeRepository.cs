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
        public Guid Save(IRecipe recipe)
        {
            using (var tran = Session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(recipe);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _session.Close();
                    _session.Dispose();
                    _session = null;

                    throw ex;
                }
            }

            return recipe.Id;
        }
    }
}
