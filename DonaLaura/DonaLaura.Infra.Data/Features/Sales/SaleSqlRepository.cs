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
            throw new NotImplementedException();
        }

        public Sale Update(Sale sale)
        {
            throw new NotImplementedException();
        }

        public Sale Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> GetAll()
        {
            throw new NotImplementedException();
        }
        
        public void Delete(Sale sale)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Sale> Make = reader =>
           new Sale
           {
               Id = Convert.ToInt32(reader["Id"]),
               //produto.Id
               ClientName = reader["Name"].ToString(),
               Quantity = Convert.ToInt32(reader["Quantity"]),
               Lucre = Convert.ToDouble(reader["Lucre"])
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="post">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Sale sale, bool hasId = true)
        {
            object[] parametros = null;

            if (true)
            {
                parametros = new object[]
                {
                    //"@Product", product.Name,
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
                  //"@Product", product.Name,
                    "@ClientName", sale.ClientName,
                    "Quantity", sale.Quantity,
                    "Lucre", sale.Lucre
                };
            }

            return parametros;
        }
    }
}
