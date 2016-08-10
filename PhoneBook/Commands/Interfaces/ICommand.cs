namespace PhoneBook.Commands
{
    public interface ICommand
    {
        string CommandKey { get; }

        string CommandDescription { get; }

        /// <summary> Может ли быть выполненна данная команда по введёной строке </summary>
        bool CanExecuteByString(string commandString);

        /// <summary> Может ли быть выполненна данная команда по введёной строке </summary>
        bool Execute(string commandString);
    }
}