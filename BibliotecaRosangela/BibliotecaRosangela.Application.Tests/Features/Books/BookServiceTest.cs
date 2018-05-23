using BibliotecaRosangela.Domain.Features.Books;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaRosangela.Common.Tests.Features.Books;
using BibliotecaRosangela.Application.Features.Books;
using FluentAssertions;
using BibliotecaRosangela.Domain.Exceptions;

namespace BibliotecaRosangela.Application.Tests.Features.Books
{
    [TestFixture]
    public class BookServiceTest
    {
        private Mock<IBookRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBookRepository>();
        }

        [Test]
        public void BookService_Add_ShouldBeOk()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();

            //Mock
            _mockRepository.Setup(m => m.Save(modelo)).Returns(new Book() { Id = 1 });

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Book resultado = service.Add(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(repository => repository.Save(modelo));
        }

        [Test]
        public void BookService_Add_Field_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Book modelo = ObjectMother.GetBookInvalidTitle();

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<BookFieldNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void BookService_Update_ShouldBeOk()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();
            modelo.Id = 1;

            //Mock
            _mockRepository.Setup(m => m.Update(modelo)).Returns(new Book() { Id = 1, Author = "Autor" });

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Book resultado = service.Update(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Author.Should().Be("Autor");
            _mockRepository.Verify(repository => repository.Update(modelo));
        }

        [Test]
        public void BookService_Update_Field_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Book modelo = ObjectMother.GetBookInvalidTitle();
            modelo.Id = 1;

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Action comparison = () => service.Update(modelo);

            //Saída
            comparison.Should().Throw<BookFieldNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void BookService_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Action comparison = () => service.Update(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void BookService_Get_ShouldBeOk()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();
            modelo.Id = 1;

            //Mock
            _mockRepository.Setup(m => m.Get(1)).Returns(new Book()
            {
                Id = 1,
                Title = "Novo livro",
                Theme = "Tema",
                Author = "Autor",
                Volume = 1,
                PublicationDate = DateTime.Now.AddYears(-3),
                Disponibility = true
            });

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Book resultado = service.Get(1);

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.Get(1));
        }

        [Test]
        public void BookService_Get_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Action comparison = () => service.Get(0);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void BookService_GetAll_ShouldBeOk()
        {
            //Cenário
            //Mock
            _mockRepository.Setup(m => m.GetAll()).Returns(new List<Book>());

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            List<Book> resultado = service.GetAll() as List<Book>;

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.GetAll());
        }

        [Test]
        public void BookService_Delete_ShouldBeOk()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();
            modelo.Id = 1;

            //Mock
            _mockRepository.Setup(m => m.Delete(modelo));

            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            service.Delete(modelo);

            //Saída
            _mockRepository.Verify(repository => repository.Delete(modelo));
        }

        [Test]
        public void BookService_Delete_Invalid_Id_ShouldBeOk()
        {
            //Cenário
            Book modelo = ObjectMother.GetBook();
            
            //Serviço
            BookService service = new BookService(_mockRepository.Object);
            //Fim Cenário

            //Executa
            Action comparison = () => service.Delete(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
