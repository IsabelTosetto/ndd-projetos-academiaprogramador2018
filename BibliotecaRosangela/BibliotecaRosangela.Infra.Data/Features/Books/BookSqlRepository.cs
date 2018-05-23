using BibliotecaRosangela.Domain.Exceptions;
using BibliotecaRosangela.Domain.Features.Books;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Infra.Data.Features.Books
{
    public class BookSqlRepository : IBookRepository
    {
        public Book Save(Book book)
        {
            book.Validate();

            string sql = "INSERT INTO TBBook(Title,Theme,Author,Volume,PublicationDate,Disponibility) " +
            "VALUES (@Title, @Theme, @Author, @Volume, @PublicationDate, @Disponibility)";

            book.Id = Db.Insert(sql, Take(book, false));

            return book;
        }

        public Book Update(Book book)
        {
            if(book.Id < 1)
                throw new IdentifierUndefinedException();

            book.Validate();

            string sql = "UPDATE TBBook SET Title = @Title, Theme = @Theme, Author = @Author, Volume = @Volume, " +
                "PublicationDate = @PublicationDate,  Disponibility = @Disponibility WHERE Id = @Id";

            Db.Update(sql, Take(book, true));
            return book;
        }

        public Book Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = "SELECT * FROM TBBook WHERE Id = @Id";

            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Book> GetAll()
        {
            string sql = "SELECT * FROM TBBook";

            return Db.GetAll(sql, Make);
        }

        public void Delete(Book book)
        {
            if(book.Id < 1)
                throw new IdentifierUndefinedException();

            string sql = "DELETE FROM TBBook WHERE Id = @Id";

            Db.Delete(sql, new object[] { "Id", book.Id });
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Book> Make = reader =>
           new Book
           {
               Id = Convert.ToInt32(reader["Id"]),
               Title = reader["Title"].ToString(),
               Theme = reader["Theme"].ToString(),
               Author = reader["Author"].ToString(),
               Volume = Convert.ToInt32(reader["Volume"]),
               PublicationDate = Convert.ToDateTime(reader["PublicationDate"]),
               Disponibility = Convert.ToBoolean(reader["Disponibility"])
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="book">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Book book, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@Title", book.Title,
                "@Theme", book.Theme,
                "@Author", book. Author,
                "@Volume", book.Volume,
                "@PublicationDate", book.PublicationDate,
                "@Disponibility", book.Disponibility,
                "@Id", book.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@Title", book.Title,
                "@Theme", book.Theme,
                "@Author", book. Author,
                "@Volume", book.Volume,
                "@PublicationDate", book.PublicationDate,
                "@Disponibility", book.Disponibility
              };
            }
            return parametros;
        }
    }
}
