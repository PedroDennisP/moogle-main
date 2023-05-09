using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine
{
    internal class Utilidades
    {
        public static List<double> MultV(double[] a, List<double> b)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Length; i++)
            {
                result.Add(a[i] * b[i]);
            }
            return result;
        }
        public static List<double> MultM(List<double[]> a, List<double> b)
        {
            double x = 0;
            List<double> result = new List<double>();
            for (int j = 0; j < a[1].Length; j++)
            {
                for (int k = 0; k < a.Count(); k++)
                {
                    x += a[k][j] * b[k];
                }
                result.Add(x);
                x= 0;
            }
            return result;


        }
    }
}
