using NHibernate;

namespace BrewRoom.Modules.Core.Repositories
{
    public class Repository
    {
        protected ISession session;

        public Repository(ISessionFactory sessionFactory)
        {
            session = sessionFactory.OpenSession();
        }
    }
}