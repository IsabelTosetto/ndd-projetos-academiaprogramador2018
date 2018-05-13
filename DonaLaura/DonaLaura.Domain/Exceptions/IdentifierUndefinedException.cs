using System;

namespace DonaLaura.Domain.Exceptions
{
    public class IdentifierUndefinedException : BusinessException
    {
        public IdentifierUndefinedException() : base("O id não pode ser vazio")
        {

        }
    }
}
