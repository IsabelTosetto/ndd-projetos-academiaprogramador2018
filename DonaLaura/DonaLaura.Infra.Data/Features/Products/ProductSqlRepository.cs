using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Features.Products
{
    public class ProductSqlRepository : IProductRepository
    {
        public Product Save(Product product)
        {
            product.Validate();

            string sql = "INSERT INTO TBProduct(Name, SalePrice, CostPrice, Disponibility, FabricationDate, ExpirationDate) " +
                "VALUES (@Name, @SalePrice, @CostPrice, @Disponibility, @FabricationDate, @ExpirationDate)";
            product.Id = Db.Insert(sql, Take(product, false));

            return product;
        }

        public Product Update(Product product)
        {
            if (product.Id < 1)
                throw new IdentifierUndefinedException();

            product.Validate();

            string sql = "UPDATE TBProduct SET Name = @Name, SalePrice = @SalePrice, CostPrice = @CostPrice," +
                " Disponibility = @Disponibility, FabricationDate = @FabricationDate, ExpirationDate = @ExpirationDate WHERE Id = @Id";
            Db.Update(sql, Take(product));

            return product;
        }

        public Product Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = "SELECT * FROM TBProduct WHERE Id = @Id";
            
            return Db.Get(sql, Make, new object[] { "@Id", id });
        }

        public IEnumerable<Product> GetAll()
        {
            string sql = "SELECT * FROM TBProduct";

            return Db.GetAll(sql, Make);
        }
        
        public void Delete(Product product)
        {
            string sql = "DELETE FROM TBProduct WHERE Id = @Id";

            if (product.Id < 1)
                throw new IdentifierUndefinedException();

            Db.Delete(sql, new object[] { "@Id", product.Id });
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Product> Make = reader =>
           new Product
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = reader["Name"].ToString(),
               SalePrice = Convert.ToDouble(reader["SalePrice"]),
               CostPrice = Convert.ToDouble(reader["CostPrice"]),
               Disponibility = Convert.ToBoolean(reader["Disponibility"]),
               FabricationDate = Convert.ToDateTime(reader["FabricationDate"]),
               ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"])
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="post">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Product product, bool hasId = true)
        {
            object[] parametros = null;

            if (true)
            {
                parametros = new object[]
                {
                    "@Name", product.Name,
                    "@SalePrice", product.SalePrice,
                    "CostPrice", product.CostPrice,
                    "Disponibility", product.Disponibility,
                    "FabricationDate", product.FabricationDate,
                    "ExpirationDate", product.ExpirationDate,
                    "@Id", product.Id,
                };
            }
            else
            {
              parametros = new object[]
              {
                  "@Name", product.Name,
                  "@SalePrice", product.SalePrice,
                  "CostPrice", product.CostPrice,
                  "Disponibility", product.Disponibility,
                  "FabricationDate", product.FabricationDate,
                  "ExpirationDate", product.ExpirationDate
              };
            }

            return parametros;
        }
    }
}
