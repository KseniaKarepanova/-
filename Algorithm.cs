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
                double[] temp_X = new double[n];
                temp_X[j - 1] = rnd.NextDouble() + point0.X[j - 1];
                for (int i = 0; i < n; i++)
                {
                    if (i != j - 1)
                        temp_X[i] = rnd.NextDouble() + point0.X[i];

                }
                double f = function.calc(temp_X);
                Point point = new Point(temp_X, f);
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
                    if ((i != j) && (Simplex[i].Function < Simplex[j].Function))
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

            double[] temp = new double[Simplex[0].X.Count()];

            for (int i = 0; i < Simplex.Count() - 1; i++)
            {
                for (int j = 0; j < Simplex[0].X.Count(); j++)
                {
                    temp[j] = temp[j] + Simplex[i].X[j];
                }
            }
            for (int i = 0; i < Simplex[0].X.Count(); i++)
            {
                temp[i] = temp[i] / Simplex[0].X.Count();
            }
            Point X_centr = new Point(temp, function.calc(temp));
            return X_centr;
        }

        //Отражение худшей точки относительно центра(пункт 4)
        public Point Reflection(Point X_centr, Point X_worst, double Alfa, IFunction function)
        {


            double[] temp = new double[X_centr.X.Count()];
            for (int i = 0; i < X_centr.X.Count(); i++)
            {
                temp[i] = (1 + Alfa) * X_centr.X[i] - Alfa * X_worst.X[i];
            }
            Point X_reflection = new Point(temp, function.calc(temp));
            return X_reflection;
        }

        public bool Dispersion(List<Point> Simplex, double eps)
        {
            double disp = 0;
            double[] temp = new double[Simplex[0].X.Count()];
            //ищем среднее значение
            for (int i = 0; i < Simplex.Count(); i++)
            {
                for (int j = 0; j < Simplex[0].X.Count(); j++)
                {
                    temp[j] = temp[j] + Simplex[i].X[j];
                }
            }
            for (int i = 0; i < Simplex[0].X.Count(); i++)
                temp[i] = temp[i] / Simplex.Count();

            //вычитаем из всех точек 
            double[] new_S = new double[Simplex[0].X.Count()];


            for (int i = 0; i < Simplex.Count(); i++)
            {
                for (int j = 0; j < Simplex[0].X.Count(); j++)
                {
                    new_S[j] = Simplex[i].X[j] - temp[j];
                }
            }

            for (int i = 0; i < Simplex[0].X.Count(); i++)
            {
                new_S[i] = Math.Pow(new_S[i], 2);
            }

            disp = new_S.Sum();
            disp = disp / (Simplex.Count() - 1);
            disp = Math.Sqrt(disp);

            if (disp <= eps) return true;
            else return false;

        }

        //Сжатие(шаг 6)
        public Point Compression(Point X_worst, Point X_centr, double Betta, IFunction function)
        {


            double[] temp = new double[X_centr.X.Count()];
            for (int i = 0; i < X_centr.X.Count(); i++)
            {
                temp[i] = Betta * X_worst.X[i] + (1 - Betta) * X_centr.X[i];
            }
            Point X_compression = new Point(temp, function.calc(temp));
            return X_compression;
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
                if (X_reflection.Function < X_best.Function)
                {
                    double[] temp = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        temp[i] = (1 - Gamma) * X_centr.X[i] + Gamma * X_reflection.X[i];
                    }
                    Point X_elongation = new Point(temp, function.calc(temp));

                    if (X_elongation.Function < X_reflection.Function)
                    {
                        X_worst = X_elongation;
                        Simplex[n] = X_worst;
                        continue;

                    }

                    else if (X_elongation.Function > X_reflection.Function)
                    {
                        X_worst = X_reflection;
                        Simplex[n] = X_worst;
                        continue;

                    }

                }

                else
                {
                    if ((X_best.Function < X_reflection.Function) && (X_reflection.Function < X_good.Function))
                    {
                        X_worst = X_reflection;
                        Simplex[n] = X_worst;

                        continue;

                    }

                    else
                    {
                        if ((X_good.Function < X_reflection.Function) && (X_reflection.Function < X_worst.Function))
                        {
                            Point temp;
                            temp = X_worst;
                            X_worst = X_reflection;
                            X_reflection = temp;

                            Simplex[n] = X_worst;

                            //6
                            X_compression = Compression(X_worst, X_centr, Betta, function);
                        }
                        else if (X_worst.Function < X_reflection.Function)
                        {

                            X_compression = Compression(X_worst, X_centr, Betta, function);

                        }
                    }
                }

                //7 
                if (X_compression.Function < X_worst.Function)
                {
                    X_worst = X_compression;
                    Simplex[n] = X_worst;
                    continue;
                }

                //8 шаг: 
                if (X_compression.Function > X_worst.Function)
                {

                    for (int i = 1; i < n + 1; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Simplex[i].X[j] = X_best.X[j] + (Simplex[i].X[j] - X_best.X[j]) / 2;

                        }

                    }

                }


            }

            Sort(Simplex);
            return (Simplex[0]);

        }





    }
}
