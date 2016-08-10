namespace PhoneBook.Sorting.Creators
{
    /// <summary> Интерфейс создания сортировщиков справочника </summary>
    public interface ISortingCreator
    {
        IPhoneBookSorter BuildPhoneBookSorter();
    }
}