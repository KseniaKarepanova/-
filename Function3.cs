using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{
    public class Function3 : IFunction  //функция Розенброка (мин (1,1) 0)
    {
        public double calc(double[] x)
        {

            return (100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2));

        }

    }
}


