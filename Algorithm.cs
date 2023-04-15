using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Нелдера_Мида
{

    public class Algorithm
    {

        //Формирование начального симплекса(1 пункт)
        public List<Point> Set_of_initial_points(int n, Point point0, IFunction function)
        {
            List<Point> Simplex = new List<Point>();

            Simplex.Add(point0);
            Random rnd = new Random();
            //Создаем рандомом n+1 точку (формируем начальный симплекс)
            for (int j = 1; j < n + 1; j++)
            {
                Point point = new Point(new double[n], function);
                point[j - 1] = rnd.NextDouble() + point0[j - 1];
                for (int i = 0; i < n; i++)
                {
                    if (i != j - 1)
                        point[i] = rnd.NextDouble() + point0[i];

                }
                Simplex.Add(point);
            }
            return Simplex;
        }

        //Функия для сортировки значений функций в порядке возрастания(2 пункт)
        public List<Point> Sort(List<Point> Simplex)
        {
            for (int i = 0; i < Simplex.Count(); i++)
            {
                for (int j = 0; j < Simplex.Count(); j++)
                {
                    if ((i != j) && (Simplex[i].Function.calc(Simplex[i].X) < Simplex[j].Function.calc(Simplex[j].X)))
                    {
                        Point temp;
                        temp = Simplex[i];
                        Simplex[i] = Simplex[j];
                        Simplex[j] = temp;
                    }

                }
            }
            return Simplex;

        }


        //Поиск центра тяжести(3 пункт)
        public Point Center_of_gravity(List<Point> Simplex, IFunction function)
        {
            Point x_centr = new Point(new double[Simplex[0].X.Count()], function);
            for (int i = 0; i < Simplex.Count() - 1; i++)
            {
                x_centr = x_centr + Simplex[i];
            }

            x_centr = x_centr / Simplex[0].X.Count();
            return x_centr;
        }

        //Отражение худшей точки относительно центра(пункт 4)
        public Point Reflection(Point X_centr, Point X_worst, double Alfa, IFunction function)
        {
            return (1 + Alfa) * X_centr - Alfa * X_worst;
        }

        public bool Dispersion(List<Point> Simplex, double eps)
        {
            double disp = 0;
            Point x_centr = new Point(new double[Simplex[0].X.Count()], Simplex[0].Function);
            //ищем среднее значение
            for (int i = 0; i < Simplex.Count(); i++)
            {
                x_centr = x_centr + Simplex[i];
            }

            x_centr = x_centr / Simplex.Count();

            //вычитаем из всех точек 
            Point new_S = new Point(new double[Simplex[0].X.Count()], Simplex[0].Function);

            for (int i = 0; i < Simplex.Count(); i++)
            {
                new_S = Simplex[i] - x_centr;
            }

            new_S = new_S * new_S;

            disp = new_S.X.Sum();
            disp = disp / (Simplex.Count() - 1);
            disp = Math.Sqrt(disp);

            if (disp <= eps) return true;
            else return false;

        }

        //Сжатие(шаг 6)
        public Point Compression(Point X_worst, Point X_centr, double Betta, IFunction function)
        {
            return Betta * X_worst + (1 - Betta) * X_centr;
        }


        public Point Elongation(Point X_reflection, Point X_centr, double Gamma, IFunction function)
        {
            return (1 - Gamma) * X_centr + Gamma * X_reflection;
        }

        public void Global_Compression(List<Point> Simplex, Point x_best)
        {
            for (int i = 1; i < Simplex.Count(); i++)
            {
                Simplex[i] = x_best + (Simplex[i] - x_best) / 2;
            }
        }

        public Point Run(int n, IFunction function, Point point0, double Alfa = 1, double Betta = 0.5, double Gamma = 2)
        {
            List<Point> Simplex = new List<Point>();

            //1 шаг: формируем начальный симплекс
            Simplex = Set_of_initial_points(n, point0, function);
            int count_step = 100;
            double eps = 0.001;

            //Условие останова - заданное количество шагов
            for (int step = 0; ((step < count_step) && (Dispersion(Simplex, eps) == false)); step++)
            {

                //2 шаг: сортирауем точки в порядке возрастания
                Simplex = Sort(Simplex);
                //выбираем лучшую, худшую и нормальную точку
                Point X_best = Simplex[0];
                Point X_good = Simplex[n - 1];
                Point X_worst = Simplex[n];

                //3 шаг: нахождение центра тяжести
                Point X_centr = Center_of_gravity(Simplex, function);

                //4 шаг: отражение худшей точки относительно центра тяжести
                Point X_reflection = Reflection(X_centr, X_worst, Alfa, function);
                //сжатая точка
                Point X_compression = Simplex[0];

                //5 шаг: Куча проверок
                if (X_reflection.Function_value < X_best.Function_value)
                {
                    Point X_elongation = Elongation(X_reflection, X_centr, Gamma, function);
                    if (X_elongation.Function_value < X_reflection.Function_value)
                    {
                        X_worst = X_elongation;
                        Simplex[n] = X_worst;
                        continue;
                    }
                    else if (X_elongation.Function_value > X_reflection.Function_value)
                    {
                        X_worst = X_reflection;
                        Simplex[n] = X_worst;
                        continue;
                    }

                }

                else if ((X_best.Function_value < X_reflection.Function_value) && (X_reflection.Function_value < X_good.Function_value))
                {
                    X_worst = X_reflection;
                    Simplex[n] = X_worst;

                    continue;
                }

                else if ((X_good.Function_value < X_reflection.Function_value) && (X_reflection.Function_value < X_worst.Function_value))
                {
                    Point temp;
                    temp = X_worst;
                    X_worst = X_reflection;
                    X_reflection = temp;

                    Simplex[n] = X_worst;
                }

                X_compression = Compression(X_worst, X_centr, Betta, function);

                //7 
                if (X_compression.Function_value < X_worst.Function_value)
                {
                    X_worst = X_compression;
                    Simplex[n] = X_worst;
                    continue;
                }

                //8 шаг: 
                if (X_compression.Function_value > X_worst.Function_value)
                {
                    Global_Compression(Simplex, X_best);
                }
            }

            Sort(Simplex);
            return (Simplex[0]);
        }

    }
}
