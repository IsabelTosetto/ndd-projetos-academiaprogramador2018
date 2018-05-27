using NUnit.Framework;
using System;
using Moq;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Domain.Features.Loans;
using FluentAssertions;

namespace BibliotecaRosangela.Domain.Test.Features.Loans
{
    [TestFixture]
    public class LoanTest
    {
        private Mock<Book> _mockBook;

        [SetUp]
        public void Setup()
        {
            _mockBook = new Mock<Book>();
        }

        [Test]
        public void Loan_Valid_ShouldBeSuccess()
        {
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.Book = _mockBook.Object;
            _mockBook.Object.Disponibility = true;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            loan.Validate();
        }

        [Test]
        public void Loan_Valid_ClienteName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "";
            loan.Book = _mockBook.Object;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            //Executa
            Action executeAction = loan.Validate;

            //Saída
            executeAction.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void Loan_Valid_ClienteName_LessThan4Characters_ShouldBeFail()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Is";
            loan.Book = _mockBook.Object;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            //Executa
            Action executeAction = loan.Validate;

            //Saída
            executeAction.Should().Throw<LoanClientNameLessThan3CharactersException>();
        }

        [Test]
        public void Loan_Valid_Book_Unavailable_ShouldBeFail()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.Book = _mockBook.Object;
            _mockBook.Object.Disponibility = false;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            //Executa
            Action executeAction = loan.Validate;

            //Saída
            executeAction.Should().Throw<LoanBookUnavailableException>();
        }

        [Test]
        public void Loan_Valid_Book_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.ReturnDate = DateTime.Now.AddDays(15);

            //Executa
            Action executeAction = loan.Validate;

            //Saída
            executeAction.Should().Throw<LoanBookNullOrEmptyException>();
        }

        [Test]
        public void Loan_Valid_ReturnDate_LowerThanCurrent_ShouldBeFail()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.Book = _mockBook.Object;
            loan.ReturnDate = DateTime.Now.AddDays(-15);

            //Executa
            Action executeAction = loan.Validate;

            //Saída
            executeAction.Should().Throw<LoanDateLowerThanCurrentException>();
        }

       [Test]
       public void Loan_Valid_Devolution_PenaltyEqualZero_ShouldBeOk()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.Book = _mockBook.Object;
            _mockBook.Object.Disponibility = true;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            DateTime devolutionDate = DateTime.Now.AddDays(12);

            double penaltyExpected = 0;

            //Executa
            loan.Devolution(devolutionDate);

            //Saída
            loan.penalty.Should().Be(penaltyExpected);
        }

        [Test]
        public void Loan_Valid_Devolution_WithPenalty_ShouldBeOk()
        {
            //Cenário
            Loan loan = new Loan();
            loan.Id = 1;
            loan.ClientName = "Isabel";
            loan.Book = _mockBook.Object;
            _mockBook.Object.Disponibility = true;
            loan.ReturnDate = DateTime.Now.AddDays(15);

            DateTime devolutionDate = DateTime.Now.AddDays(17);

            double penaltyExpected = 5;

            //Executa
            loan.Devolution(devolutionDate);

            //Saída
            loan.penalty.Should().Be(penaltyExpected);
        }

    }
}
