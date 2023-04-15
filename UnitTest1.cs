using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Алгоритм_Нелдера_Мида;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()  //Проверка 1 функции
        {
            Algorithm alg = new Algorithm();
            int n = 2;
            Function1 function = new Function1();
            double[] point = { 0, 0 };
            Point point0 = new Point(point, function.calc(point));
            double expected = -21;
            double eps = 0.001;
            Assert.IsTrue(Math.Abs(alg.Run(n, function, point0).Function - expected) <= eps);

        }



        [TestMethod]
        public void TestMethod2() //Проверка 2 функции 
        {

            Algorithm alg = new Algorithm();
            Function2 function = new Function2();
            int n = 2;
            double[] point_temp = { 0, 0 };
            Point point0 = new Point(point_temp, function.calc(point_temp));
            double expected = 0;
            double eps = 0.001;
            Assert.IsTrue(Math.Abs(alg.Run(n, function, point0).Function - expected) <= eps);

        }

        [TestMethod]
        public void TestMethod3() //Проверка 3 функции 
        {

            Algorithm alg = new Algorithm();
            Function3 function = new Function3();
            int n = 2;
            double[] point_temp = { 0, 0 };
            Point point0 = new Point(point_temp, function.calc(point_temp));
            double expected = 0;
            double eps = 0.001;
            Assert.IsTrue(Math.Abs(alg.Run(n, function, point0).Function - expected) <= eps);

        }
    }
}
