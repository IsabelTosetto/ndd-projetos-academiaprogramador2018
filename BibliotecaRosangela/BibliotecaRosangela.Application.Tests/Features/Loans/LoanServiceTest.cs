using BibliotecaRosangela.Application.Features.Loans;
using BibliotecaRosangela.Common.Tests.Features.Loans;
using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Domain.Features.Loans;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Application.Tests.Features.Loans
{
    [TestFixture]
    public class LoanServiceTest
    {
        private Mock<ILoanRepository> _mockRepository;
        private Book _book;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ILoanRepository>();
            _book = new Book();
            _book.Disponibility = true;
        }

        [Test]
        public void LoanService_Add_ShouldBeOK()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoan();
            modelo.Book = _book;

            //Mock
            _mockRepository.Setup(m => m.Save(modelo)).Returns(new Loan() { Id = 1 });
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Loan resultado = service.Add(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(repository => repository.Save(modelo));
        }

        [Test]
        public void LoanService_Add_ClientName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoanInvalidClienteName();

            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<LoanClientNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void LoanService_Update_ShouldBeOk()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoan();
            modelo.Id = 1;
            modelo.Book = _book;

            //Mock
            _mockRepository.Setup(m => m.Update(modelo)).Returns(new Loan() { Id = 1, ClientName = "Cliente" });
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Loan resultado = service.Update(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.ClientName.Should().Be(modelo.ClientName);
            _mockRepository.Verify(repository => repository.Update(modelo));
        }

        [Test]
        public void LoanService_Update_ClientName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoanInvalidClienteName();
            modelo.Id = 1;
            modelo.Book = _book;

            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(modelo);

            //Saída
            comparison.Should().Throw<LoanClientNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void LoanService_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoan();
            modelo.Book = _book;

            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void LoanService_Get_ShouldBeOk()
        {
            //Cenário

            //Mock
            _mockRepository.Setup(m => m.Get(1)).Returns(new Loan()
            {
                Id = 1,
                ClientName = "Cliente",
                ReturnDate = DateTime.Now.AddDays(15)
            });
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Loan resultado = service.Get(1);

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.Get(1));
        }

        [Test]
        public void LoanService_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Get(0);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void LoanService_GetAll_ShouldBeOk()
        {
            //Cenário

            //Mock
            _mockRepository.Setup(m => m.GetAll()).Returns(new List<Loan>());
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            List<Loan> resultado = service.GetAll() as List<Loan>;

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.GetAll());
        }

        [Test]
        public void LoanService_Delete_ShouldBeOk()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoan();
            modelo.Id = 1;
            modelo.Book = _book;

            //Mock
            _mockRepository.Setup(m => m.Delete(modelo));
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            service.Delete(modelo);

            //Saída
            _mockRepository.Verify(repository => repository.Delete(modelo));
        }

        [Test]
        public void LoanService_Delete_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            //Modelo
            Loan modelo = ObjectMother.GetLoan();
            modelo.Book = _book;
            
            //Serviço
            LoanService service = new LoanService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Delete(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
