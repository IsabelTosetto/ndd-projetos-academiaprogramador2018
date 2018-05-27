using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class LoanBookNullOrEmptyException : BusinessException
    {
        public LoanBookNullOrEmptyException() : base("O livro não pode ser nulo!")
        {

        }
    }
}
