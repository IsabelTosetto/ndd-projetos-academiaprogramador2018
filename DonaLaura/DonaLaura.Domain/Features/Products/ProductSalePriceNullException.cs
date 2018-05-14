using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductSalePriceNullException : BusinessException
    {
        public ProductSalePriceNullException() :
            base("O preço de venda deve ter um valor válido!")
        {
        }
    }
}
