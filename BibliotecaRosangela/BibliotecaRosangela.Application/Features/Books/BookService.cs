using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Books;

namespace BibliotecaRosangela.Application.Features.Books
{
    public class BookService : IBookService
    {
        private IBookRepository _repository;

        public BookService (IBookRepository repository)
        {
            _repository = repository;
        }

        public Book Add(Book book)
        {
            book.Validate();

            return _repository.Save(book);
        }

        public Book Update(Book book)
        {
            if (book.Id < 1)
                throw new IdentifierUndefinedException();

            book.Validate();

            return _repository.Update(book);
        }

        public Book Get(long id)
        {
            if(id < 1)
                throw new IdentifierUndefinedException();

            return _repository.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(Book book)
        {
            if (book.Id < 1)
                throw new IdentifierUndefinedException();

            _repository.Delete(book);
        }
    }
}
