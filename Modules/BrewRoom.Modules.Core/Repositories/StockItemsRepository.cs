using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Models;
using NHibernate;
using NHibernate.Linq;

namespace BrewRoom.Modules.Core.Repositories
{
    public class StockItemsRepository : Repository, IStockItemsRepository
    {
        public StockItemsRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public IEnumerable<IFermentable> GetGrains()
        {
            return session.Linq<Fermentable>();
        }

        public IEnumerable<IHop> GetHops()
        {
            return session.Linq<Hop>();
        }

        public Guid Save(IHop hop)
        {
            using (var tran = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(hop);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    session.Close();
                    session.Dispose();
                    session = null;

                    throw ex;
                }
            }

            return hop.Id;
        }

        public Guid Save(IFermentable fermentable)
        {
            using(var tran = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(fermentable);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    session.Close();
                    session.Dispose();
                    session = null;
                }
            }

            return fermentable.Id;
        }
    }
}
