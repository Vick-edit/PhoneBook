using System.Collections.Generic;
using System.Linq;
using PhoneBook.DataAccess;
using PhoneBook.Sorting.Algorithms;

namespace PhoneBook.Sorting
{
    public class BasePhoneBookSorter : IPhoneBookSorter
    {
        /// <summary> Объект, реализующий алгоритм сортировки </summary>
        public SorterBase<PhoneBooksCard> SortingAlgorithm { get; set; }


        public BasePhoneBookSorter(SorterBase<PhoneBooksCard> sortingAlgorithm)
        {
            SortingAlgorithm = sortingAlgorithm;
        }


        /// <summary> Отсортировать список телефонных номеров </summary>
        public List<PhoneBooksCard> SortPhoneBook(List<PhoneBooksCard> derangedList)
        {
            var derrangedArray = derangedList.ToArray();
            SortingAlgorithm.Sort(derrangedArray);

            return derrangedArray.ToList();
        }
    }
}