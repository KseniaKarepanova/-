using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{
    public class Function1 : IFunction //минимум ( (0,4) -21)
    {
        public double calc(double[] x)
        {
            return x[0] * x[0] + x[0] * x[1] + x[1] * x[1] - 6 * x[0] - 9 * x[1];

        }
    }
}
