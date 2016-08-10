namespace PhoneBook.DataAccess.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<PhoneBooksCard> PhoneBooksCardRepository();
    }
}