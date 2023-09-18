using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制网平差
{
    internal class Conversion_degree_to_radian
    {
        public static double Degree_To_Radian(double Degree)
        {
            return Degree * (Math.PI / 180.0);
        }
        public static double Radian_To_Degree(double Radian) 
        {
            return Radian * (180.0 / Math.PI);
        }
    }
}
