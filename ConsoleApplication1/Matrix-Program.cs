using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix 
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new double[,] { { 1, 3 }, { 5, 7 } };
            var m1 = new double[,] { { 4, 2 }, { 1, 5 } };
            var r = mx(m, m1);

            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    Console.Write(r[i, j] + " ");
                }
                Console.WriteLine();
            }

        }

        static double[,] mx(double[,] tx, double[,] ox)
        {
            if (tx.GetLength(1) != ox.GetLength(0))
            {
                throw new Exception("Error!");
            }

            var rx = tx.GetLength(1);
            var qx = new double[tx.GetLength(0), ox.GetLength(1)];
            for (int u = 0; u < qx.GetLength(0); u++)
                for (int io = 0; io < qx.GetLength(1); io++)
                    for (int mk = 0; mk < rx; mk++)
                        qx[u, io] += tx[u, mk] * ox[mk, io];
            return qx;
        }
    }
}