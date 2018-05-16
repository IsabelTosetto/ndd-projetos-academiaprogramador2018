using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Domain.Features.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Features.Sales
{
    public class SaleSqlRepository : ISaleRepository
    {
        public Sale Save(Sale sale)
        {
            sale.Validate();

            string sql = "INSERT INTO TBSale(ProductId, ClientName, Quantity, Lucre) VALUES(@ProductId, @ClientName, @Quantity, @Lucre)";
            sale.Id = Db.Insert(sql, Take(sale, false));

            return sale;
        }

        public Sale Update(Sale sale)
        {
            if (sale.Id < 1)
                throw new IdentifierUndefinedException();

            sale.Validate();

            string sql = "UPDATE TBSale SET ProductId = @ProductId, ClientName = @ClientName, Quantity = @Quantity," +
                " Lucre = @Lucre WHERE Id = @Id";
            Db.Update(sql, Take(sale));

            return sale;
        }

        public Sale Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = @"SELECT
                S.Id,
                S.ClientName,
                S.Quantity,
                P.Id AS ProductId,
			    P.Name AS ProductName
            FROM
                TBSale AS S
				INNER JOIN
				TBProduct AS P ON P.Id = S.ProductId
            WHERE S.Id = @Id";

            return Db.Get(sql, Make, new object[] { "@Id", id });
        }

        public IEnumerable<Sale> GetAll()
        {
            string sql = @"SELECT
                S.Id,
                S.ClientName,
                S.Quantity,
			    P.Name AS ProductName,
                P.Id AS ProductId
            FROM
                TBSale AS S
				INNER JOIN
				TBProduct AS P ON P.Id = S.ProductId";

            return Db.GetAll(sql, Make);
        }

        public void Delete(Sale sale)
        {
            string sql = "DELETE FROM TBSale WHERE Id = @Id";

            if (sale.Id < 1)
                throw new IdentifierUndefinedException();

            Db.Delete(sql, new object[] { "@Id", sale.Id });
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Sale> Make = reader =>
           new Sale
           {
               Id = Convert.ToInt32(reader["Id"]),
               ClientName = reader["ClientName"].ToString(),
               Quantity = Convert.ToInt32(reader["Quantity"]),
               Product = new Product
               {
                   Id = Convert.ToInt32(reader["ProductId"]),
                   Name = Convert.ToString(reader["ProductName"])
               }
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="post">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Sale sale, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                {
                    "@ProductId", sale.Product.Id,
                    "@ClientName", sale.ClientName,
                    "Quantity", sale.Quantity,
                    "Lucre", sale.Lucre,
                    "@Id", sale.Id,
                };
            }
            else
            {
                parametros = new object[]
                {
                    "@ProductId", sale.Product.Id,
                    "@ClientName", sale.ClientName,
                    "Quantity", sale.Quantity,
                    "Lucre", sale.Lucre
                };
            }

            return parametros;
        }
    }
}
