using PhoneBook.Sorting.AlgorithmsImplementation;

namespace PhoneBook.Sorting.Creators
{
    /// <summary> Класс для построения объекта, осуществляющего алгоритм быстрой сортировки по именам справочника </summary>
    public class QuickSortingByNameCreator : ISortingCreator
    {
        public IPhoneBookSorter BuildPhoneBookSorter()
        {
            var algorithm = new SortingPhoneBookByNames();
            var sorter = new BasePhoneBookSorter(algorithm);

            return sorter;
        }
    }
}