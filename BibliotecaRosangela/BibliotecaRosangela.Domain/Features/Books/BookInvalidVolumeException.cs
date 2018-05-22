using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class BookInvalidVolumeException : BusinessException
    {
        public BookInvalidVolumeException() : base("O volume deve ser maior que 0!")
        {

        }
    }
}
