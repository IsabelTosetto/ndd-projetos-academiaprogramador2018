﻿using BibliotecaRosangela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class LoanDateLowerThanCurrentException : BusinessException
    {
        public LoanDateLowerThanCurrentException() : base("A data de entrega deve ser maior que a atual!")
        {

        }
    }
}
