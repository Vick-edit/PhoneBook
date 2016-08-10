using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;

namespace PhoneBook.DataAccess.Repositories
{
    /// <summary> Репозиторий для доступа к цаписям телефонной книги </summary>
    public class CardRepository : IRepository<PhoneBooksCard>
    {
        public List<PhoneBooksCard> GetAll()
        {
            List<PhoneBooksCard> result;
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                var cards = session.QueryOver<PhoneBooksCard>().List();
                result = cards.ToList();
            }
            return result;
        }

        public List<PhoneBooksCard> GetByParameter(string fieldName, object searchingValue)
        {
            List<PhoneBooksCard> result;
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                var cards = session.CreateCriteria<PhoneBooksCard>()
                    .Add(Restrictions.Eq(fieldName, searchingValue))
                    .List<PhoneBooksCard>();
                result = (List<PhoneBooksCard>) cards;
            }
            return result;
        }

        public void SaveToRepository(PhoneBooksCard entity)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                using (var transation = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transation.Commit();
                }
            }
        }
    }
}