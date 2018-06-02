using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Infra;

namespace SalaDeReuniao.Common.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_TABLES =
             //"TRUNCATE TABLE [dbo].[TBLoan]; " +
             "DELETE FROM TBEmployee DBCC CHECKIDENT('TBEmployee',RESEED,0);" +
            "DELETE FROM TBRoom DBCC CHECKIDENT('TBRoom',RESEED,0);";

        private const string INSERT_EMPLOYEE = "INSERT INTO TBEmployee(Name,Position,Branch) " +
            "VALUES ('Luciane', 'Cargo', 'Ramal')";
        private const string INSERT_ROOM = "INSERT INTO TBRoom (Name,NumberOfSeats,Disponibility) VALUES ('Treinamento', 30, true)";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TABLES);
            Db.Update(INSERT_EMPLOYEE);
            Db.Update(INSERT_ROOM);
        }
    }
}
