using BibliotecaRosangela.Application.Features.Loans;
using BibliotecaRosangela.Common.Tests.Base;
using BibliotecaRosangela.Common.Tests.Features.Loans;
using b = BibliotecaRosangela.Common.Tests.Features.Books;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Domain.Features.Loans;
using BibliotecaRosangela.Infra.Data.Features.Loans;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using BibliotecaRosangela.Domain.Exceptions;

namespace BibliotecaRosangela.Integration.Tests.Features.Loans
{
    [TestFixture]
    public class LoanIntegrationTest
    {
        private ILoanService _service;
        private ILoanRepository _repository;
        private Book _book;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new LoanSqlRepository();
            _service = new LoanService(_repository);

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
            Loan result = _service.Add(loan);

            //Saída
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void LoanSqlRepository_Save_Invalid_Client_Name_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoanInvalidClienteName();
            loan.Book = _book;

            //Executa
            Action executeAction = () => _service.Add(loan);

            //Saída
            executeAction.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void LoanSqlRepository_Save_Invalid_Book_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;
            _book.Disponibility = false;

            //Executa
            Action executeAction = () => _service.Add(loan);

            //Saída
            executeAction.Should().Throw<LoanBookUnavailableException>();
        }

        [Test]
        public void LoanSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Loan loan = _service.Get(idSearch);
            loan.Book.Disponibility = true;
            string oldClienteName = loan.ClientName;
            loan.ClientName = "Novo";

            //Executa
            Loan result = _service.Update(loan);

            //Saída
            result.Should().NotBeNull();
            result.ClientName.Should().NotBe(oldClienteName);
        }

        [Test]
        public void LoanSqlRepository_Update_Invalid_ClienteName_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoanInvalidClienteName();
            loan.Id = 1;
            loan.Book = _book;
            _book.Disponibility = true;

            //Executa
            Action executeAction = () => _service.Update(loan);

            //Saída
            executeAction.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void LoanSqlRepository_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;
            _book.Disponibility = true;

            //Executa
            Action executeAction = () => _service.Update(loan);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void LoanSqlRepository_Get_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;

            //Executa
            Loan result = _service.Get(idSearch);

            //Saída
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
        }

        [Test]
        public void LoanSqlRepository_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            int idSearch = 0;

            //Executa
            Action executeAction = () => _service.Get(idSearch);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void LoanSqlRepository_GetAll_ShouldBeOk()
        {
            //Cenário
            int sizeListExpected = 1;

            //Executa
            var result = _service.GetAll();

            //Saída
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void LoanSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            int idSearch = 1;
            Loan loan = _service.Get(idSearch);
            loan.Book.Disponibility = true;

            //Executa
            _service.Delete(loan);

            //Saída
            Loan result = _service.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void LoanSqlRepository_Delete_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Loan loan = ObjectMother.GetLoan();
            loan.Book = _book;
            _book.Disponibility = true;

            //Executa
            Action executeAction = () => _service.Delete(loan);

            //Saída
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
