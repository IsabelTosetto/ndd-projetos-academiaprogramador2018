using BibliotecaRosangela.Application.Features.Books;
using BibliotecaRosangela.Common.Tests.Base;
using BibliotecaRosangela.Common.Tests.Features.Books;
using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Infra.Data.Features.Books;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Integration.Tests.Features.Books
{
    [TestFixture]
    public class BookIntegrationTest
    {
        private IBookService _service;
        private IBookRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new BookSqlRepository();
            _service = new BookService(_repository);
        }

        [Test]
        public void BookSQLIntegration_Add_ShouldBeOk()
        {
            //Cenário
            int biggerThan = 0;
            int sizeListExpected = 2;

            //Executa
            Book book = _service.Add(ObjectMother.GetBook());

            //Saída
            book.Id.Should().BeGreaterThan(biggerThan);

            var last = _service.Get(book.Id);
            last.Should().NotBeNull();

            var posts = _service.GetAll();
            posts.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void BookSQLIntegration_Add_Title_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidTitle();

            //Executa
            Action comparison = () => _service.Add(book);

            //Saída
            comparison.Should().Throw<BookFieldNullOrEmptyException>();
        }

        [Test]
        public void BookSQLIntegration_Add_Theme_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidTheme();

            //Executa
            Action comparison = () => _service.Add(book);

            //Saída
            comparison.Should().Throw<BookThemeLessThan4CharactersException>();
        }

        [Test]
        public void BookSQLIntegration_Add_Invalid_Volume_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidVolume();

            //Executa
            Action comparison = () => _service.Add(book);

            //Saída
            comparison.Should().Throw<BookInvalidVolumeException>();
        }

        [Test]
        public void BookSQLIntegration_Add_Author_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();
            book.Author = "Au";

            //Executa
            Action comparison = () => _service.Add(book);

            //Saída
            comparison.Should().Throw<BookAuthorLessThan4CharactersException>();
        }

        [Test]
        public void BookSQLIntegration_Add_PublicationDate_OverFlow_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();
            book.PublicationDate = DateTime.Now.AddYears(3);

            //Executa
            Action comparison = () => _service.Add(book);

            //Saída
            comparison.Should().Throw<BookDateOverFlowException>();
        }

        [Test]
        public void BookSQLIntegration_Update_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Book book = _service.Get(idSearch);
            string oldTheme = book.Theme;
            book.Theme = "Programação";

            //Executa
            Book result = _service.Update(book);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Theme.Should().NotBe(oldTheme);
        }

        [Test]
        public void BookSQLIntegration_Update_Invalid_Title_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidTitle();
            book.Id = 1;

            //Executa
            Action executeAction = () => _service.Update(book);

            //Saída
            executeAction.Should().Throw<BookFieldNullOrEmptyException>();
        }

        [Test]
        public void BookSQLIntegration_Update_Theme_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidTheme();
            book.Id = 1;

            //Executa
            Action executeAction = () => _service.Update(book);

            //Saída
            executeAction.Should().Throw<BookThemeLessThan4CharactersException>();
        }

        [Test]
        public void BookSQLIntegration_Update_PublicationDate_OverFlow_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();
            book.Id = 1;
            book.PublicationDate = DateTime.Now.AddYears(3);

            //Executa
            Action executeAction = () => _service.Update(book);

            //Saída
            executeAction.Should().Throw<BookDateOverFlowException>();
        }

        [Test]
        public void BookSQLIntegration_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();

            //Executa
            Action executeAction = () => _service.Update(book);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BookSQLIntegration_Get_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;

            //Executa
            Book book = _service.Get(idSearch);

            //Saída
            book.Should().NotBeNull();

            List<Book> books = _service.GetAll() as List<Book>;
            var found = books.Find(x => x.Id == book.Id);
            book.Id.Should().Be(found.Id);
        }

        [Test]
        public void BookSQLIntegration_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            int idSearch = 0;

            //Executa
            Action actionExecute = () => _service.Get(idSearch);

            //Saída
            actionExecute.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BookSQLIntegration_GetAll_ShouldBeOk()
        {
            //Cenário
            int sizeListExpected = 1;

            //Executa
            var book = _service.GetAll();

            //Saída
            book.Should().NotBeNull();
            book.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void BookSQLIntegration_Delete_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Book book = _service.Get(idSearch);

            //Executa
            _service.Delete(book);

            //Saída
            Book result = _service.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void BookSQLIntegration_Delete_Invalid_Id_ShouldBeOk()
        {
            //Cenário
            Book book = ObjectMother.GetBook();

            //Executa
            Action executeAction = () => _service.Delete(book);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
