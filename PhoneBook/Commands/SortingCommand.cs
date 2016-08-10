using PhoneBook.DataAccess;
using PhoneBook.DataAccess.Repositories;
using PhoneBook.Sorting;
using PhoneBook.UserInteraction;

namespace PhoneBook.Commands
{
    public class SortingCommand : ICommand
    {
        private readonly IRepository<PhoneBooksCard> _repository;
        private readonly IPhoneBookSorter _sorter;
        private readonly IUserInteraction _userInteraction;

        public string CommandKey
        {
            get { return "list"; }
        }

        public string CommandDescription
        {
            get { return "Просмотр отсортированного списка абонентов"; }
        }


        public SortingCommand(IRepository<PhoneBooksCard> repository, IPhoneBookSorter sorter, IUserInteraction userInteraction)
        {
            _repository = repository;
            _sorter = sorter;
            _userInteraction = userInteraction;
        }

        public bool CanExecuteByString(string commandString)
        {
            return commandString == CommandKey;
        }

        public bool Execute(string commandString)
        {
            //проверяем, что команда задана верно
            if (commandString != CommandKey) return false;

            //получаем и сортируем список номеров
            var derangedList = _repository.GetAll();
            var arrangedList = _sorter.SortPhoneBook(derangedList);

            //составляем упорядоченый список номеров
            var phoneBook = "_Имя_\t_Номер_";
            foreach (var booksCard in arrangedList)
            {
                phoneBook += string.Format("\n{0}\t{1}", booksCard.Name, booksCard.Phone);
            }
            //показываем список пользователю
            _userInteraction.SendMessage(phoneBook);

            return true;
        }
    }
}