using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{
    public class Point : ICloneable
    {
        private double[] x;
        private IFunction function;

        public Point(double[] x, IFunction function)
        {
            this.x = x;
            this.function = function;

        }
        public IFunction Function
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

        public double Function_value
        {
            get
            {
                return function.calc(x);
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

        public double this[int index]
        {
            get
            {
                return x[index];
            }
            set
            {
                x[index] = value;
            }
        }

        public static Point operator +(Point p1, Point p2)
        {

            Point p_new = new Point(new double[p1.X.Count()], p1.Function);
            for (int i = 0; i < p1.X.Count(); i++)
            {
                p_new[i] = p1[i] + p2[i];

            }

            return p_new;
        }


        public static Point operator *(double a, Point p)
        {

            Point p_new = new Point(new double[p.X.Count()], p.Function);
            for (int i = 0; i < p.X.Count(); i++)
            {
                p_new[i] = a * p[i];

            }

            return p_new;
        }

        public static Point operator /(Point p1, double a)
        {

            Point p_new = new Point(new double[p1.X.Count()], p1.Function);
            for (int i = 0; i < p1.X.Count(); i++)
            {
                p_new[i] = p1[i] / a;

            }

            return p_new;
        }

        public static Point operator -(Point p1, Point p2)
        {

            Point p_new = new Point(new double[p1.X.Count()], p1.Function);
            for (int i = 0; i < p1.X.Count(); i++)
            {
                p_new[i] = p1[i] - p2[i];

            }

            return p_new;
        }

        public static Point operator *(Point p1, Point p2)
        {

            Point p_new = new Point(new double[p1.X.Count()], p1.Function);
            for (int i = 0; i < p1.X.Count(); i++)
            {
                p_new[i] = p1[i] * p2[i];

            }

            return p_new;
        }

        public object Clone()
        {
            double[] x_copy = new double[x.Length];
            x.CopyTo(x_copy, 0);
            return new Point(x_copy, function);
        }

    }
}
