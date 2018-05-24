using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Books;
using BibliotecaRosangela.Domain.Features.Loans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Infra.Data.Features.Loans
{
    public class LoanSqlRepository : ILoanRepository
    {

        public Loan Save(Loan loan)
        {
            loan.Validate();

            string sql = "INSERT INTO TBLoan (ClientName, BookId, ReturnDate) VALUES (@ClientName, @BookId, @ReturnDate)";

            loan.Id = Db.Insert(sql, Take(loan, false));

            return loan;
        }

        public Loan Update(Loan loan)
        {
            loan.Validate();

            if(loan.Id < 1)
                throw new IdentifierUndefinedException();

            string sql = "UPDATE TBLoan SET ClientName = @ClientName, BookId = @BookId, ReturnDate = @ReturnDate WHERE Id = @Id";

            Db.Update(sql, Take(loan, true));

            return loan;
        }

        public Loan Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = "SELECT * FROM TBLoan WHERE Id = @Id";

            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Loan> GetAll()
        {
            string sql = "SELECT * FROM TBLoan";

            return Db.GetAll(sql, Make);
        }

        public void Delete(Loan loan)
        {
            if (loan.Id < 1)
                throw new IdentifierUndefinedException();

            string sql = "DELETE FROM TBLoan WHERE Id = @Id";

            Db.Delete(sql, new object[] { "Id", loan.Id });
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Loan> Make = reader =>
           new Loan
           {
               Id = Convert.ToInt32(reader["Id"]),
               ClientName = reader["ClientName"].ToString(),
               Book = new Book
               {
                   Id = Convert.ToInt32(reader["BookId"])
               },
               ReturnDate = Convert.ToDateTime(reader["ReturnDate"])
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="loan">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Loan loan, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@ClientName", loan.ClientName,
                "@BookId", loan.Book.Id,
                "@ReturnDate", loan.ReturnDate,
                "@Id", loan.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                 "@ClientName", loan.ClientName,
                "@BookId", loan.Book.Id,
                "@ReturnDate", loan.ReturnDate
              };
            }
            return parametros;
        }
    }
}
