using Moq;
using NUnit.Framework;
using PhoneBook.Commands;
using PhoneBook.UserInteraction;

namespace PhoneBook.Test.Commands
{
    [TestFixture]
    public class CommandExecutorTest
    {
        private readonly IUserInteraction _userInteraction;
        private string _errorMsg;

        public CommandExecutorTest()
        {
            _userInteraction = Mock.Of<IUserInteraction>();
            Mock.Get(_userInteraction)
                .Setup(x => x.SendMessage(It.IsAny<string>()))
                .Callback<string>((msg) => _errorMsg = msg);
        }

        [TestCase("one", "one_")]
        [TestCase("two", "_two")]
        [TestCase("three", "_three_")]
        public void TryExecuteCommand_GetCorrectString_ReturnTrue(string correctCommandKey, string commandString)
        {
            //arrange
            var commandObject = Mock.Of<ICommand>();
            Mock.Get(commandObject)
                .Setup(x => x.CanExecuteByString(It.IsAny<string>()))
                .Returns(true);
            Mock.Get(commandObject)
                .Setup(x => x.Execute(It.IsAny<string>()))
                .Returns<string>(command => command.Contains(correctCommandKey));

            var executer = new CommandExecutor(_userInteraction);
            executer.AddCommand(commandObject);


            //act
            var result = executer.TryExecuteCommand(commandString);


            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryExecuteCommand_ActOnTwoSameCommand_ReturnFalse()
        {
            //arrange
            var key = "foo";

            //Заглушка первой команды
            var commandObjectOne = Mock.Of<ICommand>(x => x.CommandKey == "one");
            Mock.Get(commandObjectOne)
                .Setup(x => x.CanExecuteByString(It.IsAny<string>()))
                .Returns<string>(commandKey => commandKey == key);

            //Заглушка второй команды
            var commandObjectTwo = Mock.Of<ICommand>(x => x.CommandKey == "two");
            Mock.Get(commandObjectTwo)
                .Setup(x => x.CanExecuteByString(It.IsAny<string>()))
                .Returns<string>(commandKey => commandKey == key);

            var executer = new CommandExecutor(_userInteraction);
            executer.AddCommand(commandObjectOne);
            executer.AddCommand(commandObjectTwo);

            _errorMsg = null;


            //act
            var result = executer.TryExecuteCommand(key);


            //assert
            Assert.That(result, Is.False);
            Assert.That(_errorMsg, Is.Not.Null);
        }

        [Test]
        public void AddCommand_AddTwiCommandsWithSameKey_SendMsg()
        {
            //arrange
            var commandObject = Mock.Of<ICommand>(x => x.CommandKey == "one");
            var executer = new CommandExecutor(_userInteraction);
            _errorMsg = null;


            //act
            executer.AddCommand(commandObject);
            executer.AddCommand(commandObject);


            //assert
            Assert.That(_errorMsg, Is.Not.Null);
        }
    }
}