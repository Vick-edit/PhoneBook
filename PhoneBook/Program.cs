using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Commands;
using PhoneBook.DataAccess;
using PhoneBook.DataAccess.Repositories;
using PhoneBook.Sorting.Creators;
using PhoneBook.UserInteraction;

namespace PhoneBook
{
    class Program
    {
        static void Main()
        {
            var userInteraction = new ConsoleInteraction();
            var commandExecutor = new CommandExecutor(userInteraction);

            var repository = new HibernateRepositoryFactory().PhoneBooksCardRepository();
            var sorter = new QuickSortingByNameCreator().BuildPhoneBookSorter();
            var sortingCommand = new SortingCommand(repository, sorter, userInteraction);
            var addingCommand = new AddingCommand(repository, userInteraction);

            commandExecutor.AddCommand(sortingCommand);
            commandExecutor.AddCommand(addingCommand);

            Console.WriteLine("Список команд телефонного справочника:");
            Console.WriteLine(commandExecutor.GetAllValidCommands());

            try
            {
                while (true)
                {
                    Console.WriteLine("\nВведите команду:");
                    var commandString = Console.ReadLine();
                    commandExecutor.TryExecuteCommand(commandString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Возникла ошибка: {0}", e.Message);
                Console.ReadKey(false);
            }

        }
    }
}
