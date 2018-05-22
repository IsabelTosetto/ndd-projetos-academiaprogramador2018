using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookFieldNullOrEmptyException : BusinessException
    {
        public BookFieldNullOrEmptyException() : base("Todos os campos devem ser preenchidos!")
        {

        }
    }
}
