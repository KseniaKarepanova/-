using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{
    public class Function2 : IFunction  //функция Химемльблау (мин (3,0) 0)
    {
        public double calc(double[] x)
        {
            return (x[0] * x[0] + x[1] - 11) * (x[0] * x[0] + x[1] - 11) + (x[0] + x[1] * x[1] - 7) * (x[0] + x[1] * x[1] - 7);

        }
    }
}