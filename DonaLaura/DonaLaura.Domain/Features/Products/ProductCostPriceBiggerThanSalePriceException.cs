using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductCostPriceBiggerThanSalePriceException : BusinessException
    {
        public ProductCostPriceBiggerThanSalePriceException() : 
            base("O preço de custo deve ser sempre menor que o preço de venda!")
        {
        }
    }
}
