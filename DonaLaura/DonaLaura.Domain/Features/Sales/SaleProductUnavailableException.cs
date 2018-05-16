using DonaLaura.Domain.Exceptions;

namespace DonaLaura.Domain.Features.Sales
{
    public class SaleProductUnavailableException : BusinessException
    {
        public SaleProductUnavailableException() : base("O produto precisa estar disponível!")
        {
        }
    }
}