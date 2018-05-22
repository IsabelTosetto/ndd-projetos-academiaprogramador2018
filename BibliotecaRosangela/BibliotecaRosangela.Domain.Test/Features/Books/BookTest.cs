using BibliotecaRosangela.Domain.Features.Books;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BibliotecaRosangela.Domain.Test.Features.Books
{
    [TestFixture]
    public class BookTest
    {

        [Test]
        public void Book_Valid_ShouldBeSuccess()
        {
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD - Testes";
            book.Theme = "Testes";
            book.Author = "Fulano";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            book.Validate();
        }

        [Test]
        public void Book_Valid_FieldNullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "";
            book.Theme = "Testes";
            book.Author = "Fulano";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookFieldNullOrEmptyException>();
        }

        [Test]
        public void Book_Valid_Title_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD";
            book.Theme = "Testes";
            book.Author = "Fulano";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookTitleLessThan4CharactersException>();
        }

        [Test]
        public void Book_Valid_Theme_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD - Testes";
            book.Theme = "Tes";
            book.Author = "Fulano";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookThemeLessThan4CharactersException>();
        }

        [Test]
        public void Book_Valid_Author_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD - Testes";
            book.Theme = "Testes";
            book.Author = "Ful";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookAuthorLessThan4CharactersException>();
        }

        [Test]
        public void Book_Valid_InvalidVolume_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD - Testes";
            book.Theme = "Testes";
            book.Author = "Fulano";
            book.Volume = -1;
            book.PublicationDate = DateTime.Now.AddYears(-3);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookInvalidVolumeException>();
        }

        [Test]
        public void Book_Valid_PublicationDate_GreaterThanCurrentDate_ShouldBeFail()
        {
            //Cenário
            Book book = new Book();
            book.Id = 1;
            book.Title = "TDD - Testes";
            book.Theme = "Testes";
            book.Author = "Fulano";
            book.Volume = 1;
            book.PublicationDate = DateTime.Now.AddYears(1);
            book.Disponibility = true;

            //Executa
            Action executeAction = book.Validate;

            //Saída
            executeAction.Should().Throw<BookDateOverFlowException>();
        }
    }
}
