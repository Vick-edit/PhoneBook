namespace PhoneBook.Sorting.Algorithms
{
    /// <summary> Абстрактный класс - контейнер для шаблонного метода - сортировки </summary>
    public abstract class SorterBase<T>
    {
        /// <summary> Точка доступа к методу сортировки </summary>
        public abstract void Sort(T[] derangedArray);

        /// <summary> Шаблонный метод сортировки </summary>
        protected abstract void Sort(T[] array, int startIndex, int stopIndex);
    }
}