using System;
using Xunit;

namespace GradeBookNEW.Tests2
{
    public class TypeTests
    {
        //// Failed attempt at writing my own test
        //[Fact]
        //public void AddGradeShouldNotAcceptDoubleOutOfRange()
        //{
        //    var grade1 = 105.12;
        //    var randomBook1 = GetBook("a book");



        //    Assert.Collection(IEnumerable<double>, randomBook1.AddGrade(105));
        //}

        int count = 0;

        public delegate string WriteLogDelegate(string logMessage);
        

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;
            //// Two next lines do the same thing. The first is a behind the scenes look at the next line of code
            log += new WriteLogDelegate(ReturnMessage);
            log += IncrementCount;

            var result = log("Hello");
            Assert.Equal(3, count);
        }



        public string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        public string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(42, x);
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCANPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(out InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name); 
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Scott";
            var upper = MakeUppercase(name);

            Assert.Equal("Scott", name);
            Assert.Equal("SCOTT", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;
            Assert.Same(book2.Name, book1.Name);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
