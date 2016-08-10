using System;

namespace PhoneBook.UserInteraction
{
    /// <summary> Класс для взаимодействия с пользователем через консоль </summary>
    public class ConsoleInteraction : IUserInteraction
    {
        private const string YesMarker = "Y";
        private const string NoMarker = "N";

        public void SendMessage(string message)
        {
            Console.WriteLine("\n{0}\n", message);
        }

        public bool YesNowQuestion(string question)
        {
            Console.WriteLine("\n{0}", question);

            var askedCounter = 0;
            do
            {
                Console.WriteLine("{0} = Да, {1} = Нет", YesMarker, NoMarker);
                var answer = Console.ReadLine();

                switch (answer)
                {
                    case YesMarker:
                        return true;
                    case NoMarker:
                        return false;
                    default:
                        Console.WriteLine("Неверная комманда, попробуйте ещё раз");
                        break;
                }
                askedCounter++;

            } while (askedCounter <= 5);

            return false;
        }
    }
}