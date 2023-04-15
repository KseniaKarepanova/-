using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Алгоритм_Нелдера_Мида
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Algorithm algorithm = new Algorithm();
            int n = 2;
            Function1 function = new Function1();
            Point point;
            Point point0 = new Point(new double[] { 0, 0 }, function);
            point=algorithm.Run(n, function, point0);

            richTextBox1.Text = richTextBox1.Text + "Point: \n";
            for(int i=0;i<n;i++)
            richTextBox1.Text = richTextBox1.Text + point[i].ToString()+"  ";

            richTextBox1.Text =richTextBox1.Text +"\n" +"Function=";
            richTextBox1.Text = richTextBox1.Text + point.Function_value.ToString();


        }
    }
}
