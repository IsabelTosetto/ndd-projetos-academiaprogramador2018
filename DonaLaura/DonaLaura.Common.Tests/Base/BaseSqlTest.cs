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
        //private const string RECREATE_SALE_TABLE =
        //    "TRUNCATE TABLE [dbo].[TBSale];";

        private const string RECREATE_PRODUCT_TABLE = 
            "TRUNCATE TABLE [dbo].[TBSale]; DELETE FROM TBProduct DBCC CHECKIDENT('DBDonaLaura.dbo.TBProduct',RESEED,0)";
        
        private const string INSERT_PRODUCT = "INSERT INTO TBProduct(Name,SalePrice,CostPrice,Disponibility,FabricationDate,ExpirationDate) " +
            "VALUES ('Produto Teste', 6, 4, 1, GETDATE(), GETDATE())";

        private const string INSERT_SALE = "INSERT INTO TBSale (ProductId,ClientName,Quantity,Lucre) VALUES (1, 'Nome Teste', 2, 2)";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_PRODUCT_TABLE);
            Db.Update(INSERT_PRODUCT);
            Db.Update(INSERT_SALE);
        }
    }
}