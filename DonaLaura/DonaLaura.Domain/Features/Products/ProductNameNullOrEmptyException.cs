using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductNameNullOrEmptyException : BusinessException
    {
        public ProductNameNullOrEmptyException() : base("O nome do produto não pode ser vazio!")
        {
        }
    }
}
