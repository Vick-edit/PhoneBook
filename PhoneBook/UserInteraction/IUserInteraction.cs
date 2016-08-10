namespace PhoneBook.UserInteraction
{
    public interface IUserInteraction
    {
        /// <summary> Отобразить сообщение пользователю </summary>
        void SendMessage(string message);

        /// <summary> Задать пользователю вопрос с варинтами Да/Нет </summary>
        /// <returns>Результат ответа пользователя</returns>
        bool YesNowQuestion(string question);
    }
}