using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Sales
{
    public class SaleClientNameNullOrEmptyException : BusinessException
    {
        public SaleClientNameNullOrEmptyException() : base("O nome do cliente não pode ser vazio!")
        {
        }
    }
}
