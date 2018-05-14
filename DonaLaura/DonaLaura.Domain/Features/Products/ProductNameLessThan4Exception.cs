using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductNameLessThan4Exception : BusinessException
    {
        public ProductNameLessThan4Exception() : 
            base("O nome do produto não pode ter menos de 4 caracteres!")
        {
        }
    }
}
