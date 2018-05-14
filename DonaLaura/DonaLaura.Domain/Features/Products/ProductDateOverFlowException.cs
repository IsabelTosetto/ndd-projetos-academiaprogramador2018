using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductDateOverFlowException : BusinessException
    {
        public ProductDateOverFlowException() :
            base("A data de fabricação deve ser menor que a atual!")
        {
        }
    }
}
