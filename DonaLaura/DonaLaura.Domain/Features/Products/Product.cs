using DonaLaura.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class Product : Entity
    {
        private string Name { get; set; }
        private double SalePrice { get; set; }
        private double PriceCusto { get; set; }
        private bool Disponibility { get; set; }
        private DateTime FabricationDate { get; set; }
        private DateTime ValidateDate { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
