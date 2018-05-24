using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class LoanClientNameNullOrEmptyException : BusinessException
    {
        public LoanClientNameNullOrEmptyException() : base("O Nome do Cliente não pode ser nulo ou vazio!")
        {

        }
    }
}
