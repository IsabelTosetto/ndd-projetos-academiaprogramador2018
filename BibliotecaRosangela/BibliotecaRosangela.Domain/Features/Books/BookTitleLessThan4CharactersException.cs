using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookTitleLessThan4CharactersException : BusinessException
    {
        public BookTitleLessThan4CharactersException() : base("O Título deve ter mais de 4 caracteres!")
        {

        }
    }
}
