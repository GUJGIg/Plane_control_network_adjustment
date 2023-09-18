using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制网平差
{
    internal class Adjustment_equation_parameters
    {
        public static void Adjustment_equation_parameter_calculation(double[] rough_coordinate_x, double[] rough_coordinate_y, float[] Original_side_length, float[] Original_plane_angle, double[] A, out double[,] Angle_adjustment_equation_parameters, out double[,] Side_length_adjustment_equation_parameters)
        {
            Angle_adjustment_equation_parameters = new double[7, 7];
            Side_length_adjustment_equation_parameters = new double[6, 5];
            double[] reverse_A = new double[7];
            for (int h = 0;h < 7;h++)
            {
                if (A[h]<180.0)
                {
                    reverse_A[h] = A[h] + 180.0;
                }
                else
                {
                    reverse_A[h] = A[h] - 180.0;
                }
            }
            int p = 206265;
            int j;
            for(int i = 0;i< 7;i++)
            {
                if(i == 0)
                {
                    for (j = 0; j < 7; j++)
                    {
                        if (j < 4)
                        {
                            Angle_adjustment_equation_parameters[i, j] = 0;
                        }
                        if (j == 4)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        if (j == 5)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        if (j == 6)
                        {
                            if (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 < 1&& -Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 > -1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600);
                            }
                            else if(-Original_plane_angle[i] - A[i + 1] + reverse_A[i] > 1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 - 360 * 3600);
                            }
                            else
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 + 360 * 3600);
                            }
                        }
                    }
                }
                if(i == 1)
                {
                    for(j = 0; j < 7; j++)
                    {
                        if(j == 2)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i])) / (1000*Original_side_length[i-1] * Original_side_length[i-1]) - (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i+2])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if(j == 3)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i])) / (1000 * Original_side_length[i-1] * Original_side_length[i-1]) + (p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i+2])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if(j == 0||j == 1)
                        {
                            Angle_adjustment_equation_parameters[i, j] = 0;
                        }
                        if(j == 4)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        if(j == 5)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        else if(j == 6)
                        {
                            if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] < 1 && -Original_plane_angle[i] - A[i + 1] + reverse_A[i] > -1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600);
                            }
                            else if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] > 1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 - 360 * 3600);
                            }
                            else
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 + 360 * 3600);
                            }
                        }
                    }
                }
                if(1 < i&&i < 5)
                {
                    for (j = 0; j < 7; j++)
                    {
                        if (j == 2)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]) - (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if (j == 3)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]) + (p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if (j == 0)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if(j == 1)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if (j == 4)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        if (j == 5)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2])) / (1000 * Original_side_length[i] * Original_side_length[i]);
                        }
                        else if (j == 6)
                        {
                            if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] < 1 && -Original_plane_angle[i] - A[i + 1] + reverse_A[i] > -1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600);
                            }
                            else if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] > 1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 - 360 * 3600);
                            }
                            else
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 + 360 * 3600);
                            }
                        }
                    }
                }
                if (i == 5)
                {
                    for (j = 0; j < 7; j++)
                    {
                        if (j == 2)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]) - (p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i - 5])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if (j == 3)
                        {
                            Angle_adjustment_equation_parameters[i, j] = ((-p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]) + (p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i - 5])) / (1000 * Original_side_length[i] * Original_side_length[i]));
                        }
                        if (j == 0)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_y[i + 1] - rough_coordinate_y[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if (j == 1)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_x[i + 1] - rough_coordinate_x[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if (j == 4||j == 5)
                        {
                            Angle_adjustment_equation_parameters[i, j] = 0;
                        }
                        else if (j == 6)
                        {
                            if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] < 1 && -Original_plane_angle[i] - A[i + 1] + reverse_A[i] > -1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600);
                            }
                            else if (-Original_plane_angle[i] - A[i + 1] + reverse_A[i] > 1)
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 - 360 * 3600);
                            }
                            else
                            {
                                Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i + 1] * 3600 + reverse_A[i] * 3600 + 360 * 3600);
                            }
                        }
                    }
                }
                else if(i == 6)
                {
                    for (j = 0; j < 7; j++)
                    {
                        if (j == 2 || j == 3)
                        {
                            Angle_adjustment_equation_parameters[i, j] = 0;
                        }
                        if (j == 0)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-p * (rough_coordinate_y[i - 6] - rough_coordinate_y[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if (j == 1)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (p * (rough_coordinate_x[i - 6] - rough_coordinate_x[i])) / (1000 * Original_side_length[i - 1] * Original_side_length[i - 1]);
                        }
                        if (j == 4|| j == 5)
                        {
                            Angle_adjustment_equation_parameters[i, j] = 0;
                        }
                        else if (j == 6)
                        {
                            Angle_adjustment_equation_parameters[i, j] = (-Original_plane_angle[i] * 3600 - A[i - 6] * 3600 + reverse_A[i] * 3600);
                        }
                    }
                }
            }
            for(int i=0;i<6;i++)
            {
                if(i == 0)
                {
                    for(j=0;j<5;j++)
                    {
                        if(j == 0 || j == 1)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = 0;
                        }
                        if(j == 2)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2]) / Original_side_length[i];
                        }
                        if(j == 3)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2]) / Original_side_length[i];
                        }
                        else if(j == 4)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (Original_side_length[i] - Math.Sqrt(Math.Pow(rough_coordinate_x[i + 2] - rough_coordinate_x[i + 1], 2) + Math.Pow(rough_coordinate_y[i + 2] - rough_coordinate_y[i + 1],2)))*1000;
                        }
                    }
                }
                if(i>0&&i<5)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (j == 0)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = -(rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2]) / Original_side_length[i];
                        }
                        if (j == 1)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = -(rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2]) / Original_side_length[i];
                        }
                        if (j == 2)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (rough_coordinate_x[i + 1] - rough_coordinate_x[i + 2]) / Original_side_length[i];
                        }
                        if (j == 3)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (rough_coordinate_y[i + 1] - rough_coordinate_y[i + 2]) / Original_side_length[i];
                        }
                        else if (j == 4)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (Original_side_length[i] - Math.Sqrt(Math.Pow(rough_coordinate_x[i + 2] - rough_coordinate_x[i + 1], 2) + Math.Pow(rough_coordinate_y[i + 2] - rough_coordinate_y[i + 1], 2)))*1000;
                        }
                    }
                }
                else if (i == 5)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (j == 0)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = -(rough_coordinate_x[i + 1] - rough_coordinate_x[i - 5]) / Original_side_length[i];
                        }
                        if (j == 1)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = -(rough_coordinate_y[i + 1] - rough_coordinate_y[i - 5]) / Original_side_length[i];
                        }
                        if (j == 2||j == 3)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = 0;
                        }
                        else if (j == 4)
                        {
                            Side_length_adjustment_equation_parameters[i, j] = (Original_side_length[i] - Math.Sqrt(Math.Pow(rough_coordinate_x[i - 5] - rough_coordinate_x[i + 1], 2) + Math.Pow(rough_coordinate_y[i - 5] - rough_coordinate_y[i + 1], 2)))*1000;
                        }
                    }
                }
            }
        }
    }
}
