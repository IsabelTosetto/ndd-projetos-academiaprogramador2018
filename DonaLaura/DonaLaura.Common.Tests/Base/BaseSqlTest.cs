using DonaLaura.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Common.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_POST_TABLE = "TRUNCATE TABLE [dbo].[TBProduct]";
        private const string INSERT_POST = "INSERT INTO TBProduct(Name,SalePrice,CostPrice,Disponibility,FabricationDate,ExpirationDate) " +
            "VALUES ('Produto Teste', 6, 4, 1, GETDATE(), GETDATE())";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_POST_TABLE);
            Db.Update(INSERT_POST);
        }
    }
}