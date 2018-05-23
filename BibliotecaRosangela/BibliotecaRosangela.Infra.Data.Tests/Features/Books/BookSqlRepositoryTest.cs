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

namespace BibliotecaRosangela.Infra.Data.Tests.Features.Books
{
    [TestFixture]
    public class BookSqlRepositoryTest
    {
        private IBookRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new BookSqlRepository();
        }

        [Test]
        public void BookSqlRepository_Save_ShouldBeOk()
        {
            //Cenário
            Book book = ObjectMother.GetBook();

            //Executa
            Book result = _repository.Save(book);

            //Saída
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void BookSqlRepository_Save_Theme_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidTheme();

            //Executa
            Action executeAction = () => _repository.Save(book);

            //Saída
            executeAction.Should().Throw<BookThemeLessThan4CharactersException>();
        }

        [Test]
        public void BookSqlRepository_Save_Invalid_Volume_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidVolume();

            //Executa
            Action executeAction = () => _repository.Save(book);

            //Saída
            executeAction.Should().Throw<BookInvalidVolumeException>();
        }

        [Test]
        public void BookSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Book book = _repository.Get(idSearch);
            book.Id = 1;
            string oldTitle = book.Title;
            book.Title = "Titulo";

            //Executa
            Book result = _repository.Update(book);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Title.Should().NotBe(oldTitle);
        }

        [Test]
        public void BookSqlRepository_Update_Invalid_Volume_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBookInvalidVolume();
            book.Id = 1;

            //Executa
            Action executeAction = () => _repository.Update(book);

            //Saída
            executeAction.Should().Throw<BookInvalidVolumeException>();
        }

        [Test]
        public void BookSqlRepository_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();

            //Executa
            Action executeAction = () => _repository.Update(book);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BookSqlRepository_Get_ShouldBeOk()
        {
            //Cenário
            int biggerThan = 0;
            int idSearch = 1;

            //Executa
            Book result = _repository.Get(idSearch);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Id.Should().BeGreaterThan(biggerThan);
        }

        [Test]
        public void BookSqlRepository_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            int idSearch = 0;

            //Executa
            Action actionExecute = () => _repository.Get(idSearch);
            //Saída
            actionExecute.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BookSqlRepository_GetAll_ShouldBeOk()
        {
            //Cenário
            int biggerThan = 0;
            int sizeListExpected = 1;
            int idFirstBookListExpected = 1;

            //Executa
            List<Book> result = _repository.GetAll() as List<Book>;

            //Saída
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
            result.First().Id.Should().Be(idFirstBookListExpected);
        }

        [Test]
        public void BookSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Book book = _repository.Get(idSearch);

            //Executa
            _repository.Delete(book);

            //Saída
            Book result = _repository.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void BookSqlRepository_Delete_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Book book = ObjectMother.GetBook();

            //Executa
            Action executeAction = () => _repository.Delete(book);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
