using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.UserInteraction;

namespace PhoneBook.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IUserInteraction _userInteraction;

        private readonly List<ICommand> _commandObjects;

        public CommandExecutor(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
            _commandObjects = new List<ICommand>();
        }


        public bool TryExecuteCommand(string command)
        {
            var executable = _commandObjects.Where(x => x.CanExecuteByString(command)).ToList();
            if (executable.Count == 0)
            {
                _userInteraction.SendMessage("Некорректная команда");
                return false;
            }
            if (executable.Count > 1)
            {
                _userInteraction.SendMessage("Невозможно выполнить команду, более одной операции может быть выполненно по введёной строке");
                return false;
            }

            return executable[0].Execute(command);
        }

        public string GetAllValidCommands()
        {
            var commandKeys = "";

            foreach (var command in _commandObjects)
            {
                commandKeys += String.Format("{0} - {1}\n", command.CommandKey, command.CommandDescription);
            }

            return commandKeys;
        }

        public void AddCommand(ICommand commandObject)
        {
            var keyConflict = _commandObjects.Any(x => x.CommandKey == commandObject.CommandKey);
            if (keyConflict)
            {
                _userInteraction.SendMessage("Невозможно добавить команду, т.к. объект уже содержит команду с таким ключом");
                return;
            }

            _commandObjects.Add(commandObject);
        }
    }
}