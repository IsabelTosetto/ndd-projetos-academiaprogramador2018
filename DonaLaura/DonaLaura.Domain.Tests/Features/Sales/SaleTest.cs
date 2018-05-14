using DonaLaura.Domain.Features.Sales;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Tests.Features.Sales
{
    [TestFixture]
    public class SaleTest
    {
        Sale sale;

        [SetUp]
        public void Setup()
        {
            sale = new Sale();
        }



        [TearDown]
        public void TearDown()
        {
            sale = null;
        }
    }
}
