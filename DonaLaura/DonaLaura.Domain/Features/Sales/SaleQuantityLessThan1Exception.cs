using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Sales
{
    public class SaleQuantityLessThan1Exception : BusinessException
    {
        public SaleQuantityLessThan1Exception() : base("A quantidade deve ser maior que 0!")
        {
        }
    }
}
