using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class LoanBookUnavailableException : BusinessException
    {
        public LoanBookUnavailableException() : base("O Livro deve estar disponível para empréstimo!")
        {

        }
    }
}
