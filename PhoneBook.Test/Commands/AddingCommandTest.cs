using Moq;
using NUnit.Framework;
using PhoneBook.Commands;
using PhoneBook.DataAccess;
using PhoneBook.DataAccess.Repositories;
using PhoneBook.UserInteraction;

namespace PhoneBook.Test.Commands
{
    [TestFixture]
    public class AddingCommandTest
    {
        private readonly IRepository<PhoneBooksCard> _repository;
        private readonly IUserInteraction _userInteraction;

        public AddingCommandTest()
        {
            _repository = Mock.Of<IRepository<PhoneBooksCard>>();

            _userInteraction = Mock.Of<IUserInteraction>();
            Mock.Get(_userInteraction)
                .Setup(x => x.YesNowQuestion(It.IsAny<string>()))
                .Returns<string>((msg) => true);
        }

        [TestCase("addadd Name 911")]
        [TestCase(" add  Name ")]
        [TestCase(" add  911 ")]
        public void Execute_PassWrongCommandAndValues_ReturnFalse(string commandString)
        {
            var result = ArrangeAndAct(commandString);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCase("   add  Name   911   ")]
        [TestCase("  add   add - Name   911   ")]
        public void Execute_PassCorrectCommandAndValues_ReturnTrue(string commandString)
        {
            var result = ArrangeAndAct(commandString);

            //assert
            Assert.That(result, Is.True);
        }

        private bool ArrangeAndAct(string commandString)
        {
            //arrange
            var commandObject = new AddingCommand(_repository, _userInteraction);

            //act
            var result = commandObject.Execute(commandString);
            return result;
        }
    }
}