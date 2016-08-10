using System.Linq;
using Moq;
using NUnit.Framework;
using PhoneBook.DataAccess;
using PhoneBook.Sorting.AlgorithmsImplementation;

namespace PhoneBook.Test.Sorting
{
    [TestFixture]
    public class SortingPhoneBookByNamesTest
    {
        [Test]
        public void Sort_SortCardsByName_CorrectSequence()
        {
            //arrange
            var arrayToSort = new[] { "два", "Четыре", "", "Три", "Два", null, "Три" };
            var arraySorted = new[] { null, "", "два", "Два", "Три", "Три", "Четыре" };

            var tuplesToSort = arrayToSort.Select(x => Mock.Of<PhoneBooksCard>(card => card.Name == x)).ToArray();
            var sorter = new SortingPhoneBookByNames();


            //act
            sorter.Sort(tuplesToSort);
            var sortingResult = tuplesToSort.Select(x => x.Name).ToArray();


            //assert
            for (var i = 0; i < arraySorted.Length; ++i)
            {
                Assert.That(sortingResult[i], Is.EqualTo(arraySorted[i]));
            }
        }
    }
}