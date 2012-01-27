using NHibernate;

namespace BrewRoom.Modules.Core.Repositories
{
    public class Repository
    {
        protected readonly ISessionFactory _sessionFactory;
        protected ISession _session;

        public ISession Session
        {
            get
            {
                if (_session == null)
                    return _session = _sessionFactory.OpenSession();
                return _session;
            }
        }

        public Repository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
    }
}