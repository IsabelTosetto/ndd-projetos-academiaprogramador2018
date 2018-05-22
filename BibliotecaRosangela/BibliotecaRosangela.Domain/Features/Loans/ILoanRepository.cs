using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public interface ILoanRepository
    {
        Loan Save(Loan loan);
        Loan Update(Loan loan);
        Loan Get(long id);
        IEnumerable<Loan> GetAll();
        void Delete(Loan loan);
    }
}
