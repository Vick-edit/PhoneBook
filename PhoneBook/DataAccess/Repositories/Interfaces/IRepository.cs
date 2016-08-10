using System.Collections.Generic;

namespace PhoneBook.DataAccess.Repositories
{
    /// <summary> Интерфейс доступа к Entity БД </summary>
    /// <typeparam name="T">Entity БД</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary> Получить все записи </summary>
        List<T> GetAll();

        /// <summary> Получить записи поле которых соответствует указанному значению </summary>
        List<T> GetByParameter(string fieldName, object searchingValue);

        /// <summary> Сохранить запись в телефонную книгу </summary>
        void SaveToRepository(T entity);
    }
}