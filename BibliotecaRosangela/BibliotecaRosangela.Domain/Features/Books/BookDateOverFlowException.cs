using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookDateOverFlowException : BusinessException
    {
        public BookDateOverFlowException() : base("A Data de Publicação deve ser menor que a data atual!")
        {

        }
    }
}
