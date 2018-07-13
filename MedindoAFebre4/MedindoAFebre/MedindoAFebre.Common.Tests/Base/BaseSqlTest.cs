using MedindoAFebre.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedindoAFebre.Common.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_TABLES = "";

        private const string INSERT_ = "";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TABLES);
        }
    }
}
