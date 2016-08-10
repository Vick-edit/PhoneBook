namespace PhoneBook.Commands
{
    public interface ICommandExecutor
    {
        /// <summary> Попытаться выполнить команду, соответствующую данному выражению </summary>
        bool TryExecuteCommand(string command);

        /// <summary> Получить список допустимых команд </summary>
        string GetAllValidCommands();

        /// <summary> Добавить новую исполняемую команду </summary>
        void AddCommand(ICommand commandObject);
    }
}