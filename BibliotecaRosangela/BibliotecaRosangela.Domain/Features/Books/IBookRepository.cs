using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public interface IBookRepository
    {
        Book Save(Book book);
        Book Update(Book book);
        Book Get(long id);
        IEnumerable<Book> GetAll();
        void Delete(Book book);
    }
}
