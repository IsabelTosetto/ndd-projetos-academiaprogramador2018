using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Loans
{
    public class Loan
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public Book Book { get; set; }
        public DateTime ReturnDate { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ClientName))
                throw new LoanClienteNameNullOrEmptyException();

            if (ClientName.Length < 3)
                throw new LoanClientNameLessThan3CharactersException();

            if (ReturnDate.CompareDateSmallerCurrent())
                throw new LoanDateLowerThanCurrentException();

            if (Book.Disponibility == false)
                throw new LoanBookUnavailableException();
        }
    }
}
