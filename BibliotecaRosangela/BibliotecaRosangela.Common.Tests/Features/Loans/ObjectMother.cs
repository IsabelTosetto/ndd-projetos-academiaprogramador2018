using BibliotecaRosangela.Domain.Features.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Common.Tests.Features.Loans
{
    public static partial class ObjectMother
    {
        public static Loan GetLoan()
        {
            return new Loan()
            {
                ClientName = "Cliente",
                ReturnDate = DateTime.Now.AddDays(15)
            };
        }
    }
}
