﻿using BibliotecaRosangela.Domain.Features.Books;
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
        public double penalty;

        public void Validate()
        {
            if (string.IsNullOrEmpty(ClientName))
                throw new LoanClientNameNullOrEmptyException();

            if (ClientName.Length < 3)
                throw new LoanClientNameLessThan3CharactersException();

            if (ReturnDate.CompareDateSmallerCurrent())
                throw new LoanDateLowerThanCurrentException();

            if (Book == null)
                throw new LoanBookNullOrEmptyException();

            if (Book.Disponibility == false)
                throw new LoanBookUnavailableException();

        }

        public void Devolution(DateTime devolutionDate) //CalcularMulta
        {
            TimeSpan total = ReturnDate.Subtract(devolutionDate);
            int totalDays;
            if(total.Days < 1)
            {
                totalDays = total.Days * -1;
                penalty = totalDays * 2.50;
            } else
            {
                penalty = 0;
            }
        }
    }
}
