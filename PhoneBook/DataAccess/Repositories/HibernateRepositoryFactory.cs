namespace PhoneBook.DataAccess.Repositories
{
    public class HibernateRepositoryFactory : IRepositoryFactory
    {
        public IRepository<PhoneBooksCard> PhoneBooksCardRepository()
        {
            return new CardRepository();
        }
    }
}