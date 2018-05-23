using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Loans;

namespace BibliotecaRosangela.Application.Features.Loans
{
    public class LoanService : ILoanService
    {
        private ILoanRepository _repository;

        public LoanService(ILoanRepository repository)
        {
            _repository = repository;
        }

        public Loan Add(Loan loan)
        {
            loan.Validate();

            return _repository.Save(loan);
        }

        public Loan Update(Loan loan)
        {
            if (loan.Id < 1)
                throw new IdentifierUndefinedException();

            loan.Validate();

            return _repository.Update(loan);
        }

        public Loan Get(long id)
        {
            if(id < 1)
                throw new IdentifierUndefinedException();

            return _repository.Get(1);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _repository.GetAll();
        }
        
        public void Delete(Loan loan)
        {
            if (loan.Id < 1)
                throw new IdentifierUndefinedException();

            _repository.Delete(loan);
        }
    }
}
