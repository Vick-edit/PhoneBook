using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace PhoneBook.DataAccess
{
    public static class FluentNHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var dbConfig = SQLiteConfiguration.Standard.UsingFile("phonebook.db");

                    _sessionFactory = Fluently.Configure()
                        .Database(dbConfig)
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PhoneBooksCard>())
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}