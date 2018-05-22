using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Infra
{
    public static class DateHelper
    {
        public static bool CompareDateSmallerCurrent(this DateTime dt)
        {
            int result = DateTime.Compare(dt, DateTime.Now);
            if (result <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
