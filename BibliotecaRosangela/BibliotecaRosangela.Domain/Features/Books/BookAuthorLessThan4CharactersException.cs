using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookAuthorLessThan4CharactersException : BusinessException
    {
        public BookAuthorLessThan4CharactersException() : base("O Autor deve ter mais de 4 caracteres!")
        {

        }
    }
}
