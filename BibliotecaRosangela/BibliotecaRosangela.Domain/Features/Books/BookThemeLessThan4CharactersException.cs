using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookThemeLessThan4CharactersException : BusinessException
    {
        public BookThemeLessThan4CharactersException() : base("O Tema deve ter mais de 4 caracteres!")
        {

        }
    }
}
