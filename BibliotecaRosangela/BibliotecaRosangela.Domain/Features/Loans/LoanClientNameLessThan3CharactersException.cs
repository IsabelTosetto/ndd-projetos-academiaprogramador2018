using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class LoanClientNameLessThan3CharactersException : BusinessException
    {
        public LoanClientNameLessThan3CharactersException() : base("O Nome do Cliente deve ter mais de 4 caracteres!")
        {

        }
    }
}
