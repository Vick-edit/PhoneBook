using System;
using System.ComponentModel.DataAnnotations;
using NHibernate.Util;
using PhoneBook.DataAccess;
using PhoneBook.DataAccess.Repositories;
using PhoneBook.Extensions;
using PhoneBook.UserInteraction;

namespace PhoneBook.Commands
{
    public class AddingCommand : ICommand
    {
        private readonly IRepository<PhoneBooksCard> _repository;
        private readonly IUserInteraction _userInteraction;

        public string CommandKey
        {
            get { return "add"; }
        }

        public string CommandDescription
        {
            get { return "Добавление информации в телефонный справочник. \n" +
                         "\tФормат ввода \"add <Name> <Phone>\", номер без пробелов"; }
        }


        public AddingCommand(IRepository<PhoneBooksCard> repository, IUserInteraction userInteraction)
        {
            _repository = repository;
            _userInteraction = userInteraction;
        }


        public bool CanExecuteByString(string commandString)
        {
            if (commandString == null) return false;
            return commandString.TrimStart().StartsWith(CommandKey);
        }

        public bool Execute(string commandString)
        {
            if (!CanExecuteByString(CommandKey)) return false;

            //парсим строку
            string name, phone;
            var parsed = ParseCommand(commandString, out name, out phone);
            if (!parsed) return false;

            //проверяем распарсенные данные
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                ShowToUser("Добавляемые Имя или Телефон не могут быть пустыми:\n" + commandString);
                return false;
            }

            //проверяем, существуют ли уже записи с таким именем
            var propertyName = ClassInfo.GetMemberName((PhoneBooksCard c) => c.Name);
            var oldPhones = _repository.GetByParameter(propertyName, name);
            if (oldPhones != null && oldPhones.Count > 1)
            {
                ShowToUser(string.Format("Невозможно добавить запись с именем {0}, т.к. БД уже содержит более одной такой записи", name));
                return false;
            }

            //Сохраняем или обновляем
            if (oldPhones == null || oldPhones.Count == 0)
                return AddNewPhoneBooksCard(name, phone);

            if (oldPhones != null && oldPhones.Count == 1)
                return UpdatePhoneBooksCard(oldPhones[0], phone);

            return false;
        }

        private bool ParseCommand(string commandString, out string name, out string phone)
        {
            //удаляем ключ команды из строки
            commandString = commandString.Trim();
            var keyIndex = commandString.IndexOf(CommandKey);
            if (keyIndex != 0 || commandString.Length <= CommandKey.Length || commandString[CommandKey.Length] != ' ')
            {
                ShowToUser("Неверный формат комманды добавления записи:\n" + commandString);
                name = phone = null;
                return false;
            }
            var nameAndPhone = commandString.Substring(CommandKey.Length + 1);

            //Извлекаем Имя и Телефон
            var lastSpaceIndex = nameAndPhone.LastIndexOf(' ');
            name = nameAndPhone.Substring(0, lastSpaceIndex).Trim();
            phone = nameAndPhone.Substring(lastSpaceIndex).Trim();
            return true;
        }

        private bool AddNewPhoneBooksCard(string name, string phone)
        {
            var addNew = _userInteraction.YesNowQuestion(string.Format("Добавить в справочник запись:\n" +
                                                                        "Имя:{0} Телефон:{1}?", name, phone));
            if (addNew)
                try
                {
                    var newPhoneBooksCard = new PhoneBooksCard();
                    newPhoneBooksCard.Name = name;
                    newPhoneBooksCard.Phone = phone;
                    _repository.SaveToRepository(newPhoneBooksCard);
                    ShowToUser("Запись успешно добавлена");
                }
                catch (Exception e)
                {
                    ShowToUser("Ошибка: " + e.Message);
                    return false;
                }

            return true;
        }

        private bool UpdatePhoneBooksCard(PhoneBooksCard oldPhone, string phone)
        {
            var update = _userInteraction.YesNowQuestion(string.Format("Заменить для {0} номер телефона?\n" +
                                                                        "Старый номер:{1} новый номер:{2}?"
                                                                        , oldPhone.Name, oldPhone.Phone, phone));
            if (update)
                try
                {
                    oldPhone.Phone = phone;
                    _repository.SaveToRepository(oldPhone);
                    ShowToUser("Запись успешно обновлена");
                }
                catch (Exception e)
                {
                    ShowToUser("Ошибка: " + e.Message);
                    return false;
                }

            return true;
        }

        private void ShowToUser(string message)
        {
            _userInteraction.SendMessage(message);
        }
    }
}