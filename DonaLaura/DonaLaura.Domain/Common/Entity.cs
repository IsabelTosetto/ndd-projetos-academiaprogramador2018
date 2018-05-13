using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Common
{
    public abstract class Entity
    {
        protected int Id { get; set; }

        protected abstract void Validate();
    }
}
