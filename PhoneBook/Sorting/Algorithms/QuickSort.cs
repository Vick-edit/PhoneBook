namespace PhoneBook.Sorting.Algorithms
{
    /// <summary>
    /// Абстрактный класс - контейнер для алгоритма быстрой сортировки
    /// </summary>
    public abstract class QuickSort<T> : SorterBase<T>
    {
        public sealed override void Sort(T[] derangedArray)
        {
            if (derangedArray.Length < 2) return;

            var startIndex = 0;
            var stopIndex = derangedArray.Length - 1;
            Sort(derangedArray, startIndex, stopIndex);
        }

        //Шаблонный метод - алгоритм сортировки
        protected sealed override void Sort(T[] array, int startIndex, int stopIndex)
        {
            if (startIndex >= stopIndex) return;

            //Сначала выбираем опорный элемент
            var pivotIndex = startIndex;
            var pivotItem = array[pivotIndex];

            //Устанавливаем индексы, для итерации элементов
            int leftIndex = startIndex + 1;
            int rightIndex = stopIndex;

            //Меняем местами опорный элемент и другие элементы пока он не займёт своё место
            while (leftIndex <= rightIndex)
            {
                //Перебираем элементы справа, пока не найдём элемент равный или меньше опорного элемента
                //или пока не достигнем опорного элемента
                var rightItem = array[rightIndex];
                while (rightIndex > pivotIndex && IsFirstBigger(rightItem, pivotItem))
                {
                    rightIndex--;
                    rightItem = array[rightIndex];
                }
                //Если правый индекс получился больше индекса опорного - меняем элементы местами
                if (rightIndex > pivotIndex)
                {
                    SwapElements(array, rightIndex, pivotIndex);
                    pivotIndex = rightIndex;
                    rightIndex--;
                }

                //Теперь перебираем элементы слева, пока не найдём элемент больше опорного элемента
                //или пока не достигнем опорного элемента
                var leftItem = array[leftIndex];
                while (leftIndex < pivotIndex && IsFirstLessOrEqual(leftItem, pivotItem))
                {
                    leftIndex++;
                    leftItem = array[leftIndex];
                }
                //Если левый индекс получился меньше индекса опорного - меняем элементы местами
                if (leftIndex < pivotIndex)
                {
                    SwapElements(array, leftIndex, pivotIndex);
                    pivotIndex = leftIndex;
                    leftIndex++;
                }

                //И снова начинаем перебирать элементы справа и так, пока левый и правый индексы не сомкнуться
            }

            //Теперь сортируем элементы справа от опорного и слева
            Sort(array, startIndex, pivotIndex - 1);
            Sort(array, pivotIndex + 1, stopIndex);
        }

        //Функция меняет местами элементы массива
        private void SwapElements(T[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        protected abstract bool IsFirstBigger(T firstItem, T secondItem);

        protected abstract bool IsFirstLessOrEqual(T firstItem, T secondItem);
    }
}