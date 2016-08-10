using System;
using PhoneBook.DataAccess;
using PhoneBook.Sorting.Algorithms;

namespace PhoneBook.Sorting.AlgorithmsImplementation
{
    ///<summary> Класс для сортировки телефонных номеров по Имени </summary>
    public class SortingPhoneBookByNames : QuickSort<PhoneBooksCard>
    {
        private const StringComparison ComparisonRule = StringComparison.CurrentCulture;


        protected override bool IsFirstBigger(PhoneBooksCard firstItem, PhoneBooksCard secondItem)
        {
            var firstString = firstItem.Name;
            var secondString = secondItem.Name;

            return string.Compare(firstString, secondString, ComparisonRule) > 0;
        }

        protected override bool IsFirstLessOrEqual(PhoneBooksCard firstItem, PhoneBooksCard secondItem)
        {
            var firstString = firstItem.Name;
            var secondString = secondItem.Name;

            return string.Compare(firstString, secondString, ComparisonRule) <= 0;
        }
    }
}