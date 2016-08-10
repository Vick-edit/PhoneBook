using System.Collections.Generic;
using PhoneBook.DataAccess;

namespace PhoneBook.Sorting
{
    public interface IPhoneBookSorter
    {
        List<PhoneBooksCard> SortPhoneBook(List<PhoneBooksCard> derangedList);
    }
}