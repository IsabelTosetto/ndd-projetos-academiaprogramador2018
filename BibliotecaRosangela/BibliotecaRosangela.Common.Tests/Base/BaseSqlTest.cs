using BibliotecaRosangela.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Common.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_TABLES =
             "TRUNCATE TABLE [dbo].[TBLoan]; DELETE FROM TBBook DBCC CHECKIDENT('TBBook',RESEED,0)";

        private const string INSERT_BOOK = "INSERT INTO TBBook(Title,Theme,Author,Volume,PublicationDate,Disponibility) " +
            "VALUES ('Livro Teste', 'Tema Teste', 'Autor Teste', 1, GETDATE(), true)";
        private const string INSERT_LOAN = "INSERT INTO TBLoan (ClientName,BookId,ReturnDate) VALUES ('Nome Teste', 1, GETDATE())";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TABLES);
            Db.Update(INSERT_BOOK);
            Db.Update(INSERT_LOAN);
        }
    }
}
