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
            IHop hop1 = new Hop("Saaz");
            hop1.AddOilCharacteristics(new HopOilCharacteristics
            {
                Carophyllene = 20M,
                Farnesene = 20M,
                Humulene = 20M,
                Myrcene = 20M,
                OtherAcids = 20M,
                PercentageOfTotalWeight = 20,
                TotalAlphaAcid = 5M
            });

            IHop hop2 = new Hop("Cascade");
            hop2.AddOilCharacteristics(new HopOilCharacteristics
            {
                Carophyllene = 20M,
                Farnesene = 20M,
                Humulene = 20M,
                Myrcene = 20M,
                OtherAcids = 20M,
                PercentageOfTotalWeight = 20,
                TotalAlphaAcid = 6.5M
            });

            var hops = new List<IHop>
                        {
                            hop1,
                            hop2
                        };

            return hops;
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
