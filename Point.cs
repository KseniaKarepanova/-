using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{
    public class Point
    {
        double[] x;
        double function;
        public Point(double[] x, double function)
        {
            this.x = x;
            this.function = function;

        }

        public double Function
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
            }
        }
        public double[] X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }


    }
}
