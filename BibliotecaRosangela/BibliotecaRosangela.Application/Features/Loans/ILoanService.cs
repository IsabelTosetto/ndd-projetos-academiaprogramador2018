﻿using BibliotecaRosangela.Domain.Features.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Application.Features.Loans
{
    public interface ILoanService
    {
        Loan Add(Loan loan);
        Loan Update(Loan loan);
        Loan Get(long id);
        IEnumerable<Loan> GetAll();
        void Delete(Loan loan);
    }
}
