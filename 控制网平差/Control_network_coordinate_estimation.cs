using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制网平差
{
    internal class Control_network_coordinate_estimation
    {
        public static void Coordinate_estimate(float[] Original_plane_angle, float[] Original_side_length, float[] starting_point_coordinates_x, float[] starting_point_coordinates_y, out double[] rough_coordinate_x, out double[] rough_coordinate_y, out double[] A)
        {
            rough_coordinate_x = new double[7];
            rough_coordinate_y = new double[7];
            A = new double[7];
            double[] radian_A = new double[7];
            double A_starting;
            rough_coordinate_x[0] = starting_point_coordinates_x[0];
            rough_coordinate_y[0] = starting_point_coordinates_y[0];
            rough_coordinate_x[1] = starting_point_coordinates_x[1];
            rough_coordinate_y[1] = starting_point_coordinates_y[1];
            A_starting = (Math.Atan((starting_point_coordinates_y[1] - starting_point_coordinates_y[0]) / (starting_point_coordinates_x[1] - starting_point_coordinates_x[0])))*(180.0/Math.PI);
            if(A_starting<0)
            {
                A_starting = A_starting+360;
            }
            A[0] = A_starting;
            radian_A[0] = Conversion_degree_to_radian.Degree_To_Radian(A_starting);
            for(int i = 0;i<6;i++) 
            {
                A[i+1] = A[i] - Original_plane_angle[i] + 180.0;
                radian_A[i + 1] = Conversion_degree_to_radian.Degree_To_Radian(A[i+1]);
            }
            int l = 2;
            double M = Math.Cos(radian_A[l - 1]) * Original_side_length[l - 2];
            for (int i = 2;i<7;i++)
            {
                rough_coordinate_x[i] = rough_coordinate_x[i-1] + Math.Cos(radian_A[i - 1]) * Original_side_length[i-2];
                rough_coordinate_y[i] = rough_coordinate_y[i-1] + Math.Sin(radian_A[i - 1]) * Original_side_length[i-2];
            }
        }
    }
}
