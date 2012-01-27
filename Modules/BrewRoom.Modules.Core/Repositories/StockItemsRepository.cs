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

        public IEnumerable<IFermentable> GetStockFermentables()
        {
            return Session.Linq<StockFermentable>();
        }

        public IEnumerable<IHop> GetHops()
        {
            return Session.Linq<StockHop>();
        }

        public Guid Save(IHop hop)
        {
            using (var tran = Session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(hop);
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

            return hop.Id;
        }

        public Guid Save(IFermentable fermentable)
        {
            using(var tran = Session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(fermentable);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    _session.Close();
                    _session.Dispose();
                    _session = null;
                }
            }

            return fermentable.Id;
        }
    }
}
