using BibliotecaRosangela.Common.Tests.Base;
using BibliotecaRosangela.Common.Tests.Features.Loans;
using b = BibliotecaRosangela.Common.Tests.Features.Books;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Domain.Features.Loans;
using BibliotecaRosangela.Infra.Data.Features.Loans;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaRosangela.Domain.Exceptions;

namespace BibliotecaRosangela.Infra.Data.Tests.Features.Loans
{
    [TestFixture]
    public class LoanSqlRepositoryTest
    {
        private ILoanRepository _repository;
        private Book _book;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new LoanSqlRepository();
            _book = b.ObjectMother.GetBook();
            _book.Id = 1;
        }

        [Test]
        public void LoanSqlRepository_Save_ShouldBeOk()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;

            //Executa
            Loan result = _repository.Save(loan);

            //Saída
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void LoanSqlRepository_Save_ClientName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoanInvalidClienteName();
            loan.Book = _book;

            //Executa
            Action executeAction = () => _repository.Save(loan);

            //Saída
            executeAction.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void LoanSqlRepository_Save_Book_Unavailable_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;
            _book.Disponibility = false;

            //Executa
            Action executeAction = () => _repository.Save(loan);

            //Saída
            executeAction.Should().Throw<LoanBookUnavailableException>();
        }

        [Test]
        public void LoanSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Loan loan = _repository.Get(idSearch);
            loan.Id = 1;
            loan.Book.Disponibility = true;
            string oldClientName = loan.ClientName;
            loan.ClientName = "Novo nome";

            //Executa
            Loan result = _repository.Update(loan);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.ClientName.Should().NotBe(oldClientName);
        }

        [Test]
        public void LoanSqlRepository_Update_Book_Unavailable_ShouldBeOk()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Id = 1;
            loan.Book = _book;
            _book.Disponibility = false;

            //Executa
            Action executeAction = () => _repository.Update(loan);

            //Saída
            executeAction.Should().Throw<LoanBookUnavailableException>();
        }

        [Test]
        public void LoanSqlRepository_Update_Invalid_Id_ShouldBeOk()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;

            //Executa
            Action executeAction = () => _repository.Update(loan);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void LoanSqlRepository_Get_ShouldBeOk()
        {
            //Cenário
            int biggerThan = 0;
            int idSearch = 1;

            //Executa
            Loan result = _repository.Get(idSearch);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Id.Should().BeGreaterThan(biggerThan);
        }

        [Test]
        public void LoanSqlRepository_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            int idSearch = 0;

            //Executa
            Action actionExecute = () => _repository.Get(idSearch);
            //Saída
            actionExecute.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void LoanSqlRepository_GetAll_ShouldBeOk()
        {
            //Cenário
            int sizeListExpected = 1;
            int idFirstLoanListExpected = 1;

            //Executa
            List<Loan> result = _repository.GetAll() as List<Loan>;

            //Saída
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
            result.First().Id.Should().Be(idFirstLoanListExpected);
        }

        [Test]
        public void LoanSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Loan loan = _repository.Get(idSearch);

            //Executa
            _repository.Delete(loan);

            //Saída
            Loan result = _repository.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void LoanSqlRepository_Delete_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();

            //Executa
            Action executeAction = () => _repository.Delete(loan);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
