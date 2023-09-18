using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace 控制网平差
{
    public partial class Form1 : Form
    {
        string[] Original_plane_angle = new string[7];
        string[] Original_side_length = new string [7];
        string[] starting_point_coordinates_x = new string[7];
        string[] starting_point_coordinates_y = new string[7];
        double[,] B = new double[13,10];
        double[,] l = new double[13,1];
        double[] V = new double[13];
        double[] V_side_length = new double[6];
        double[] V_plane_angle = new double[7];
        double[] T_side_length = new double[6];
        double[] T_plane_angle = new double[7];
        double[] V_x = new double[10];
        double[] T_point_coordinates_x = new double[7];
        double[] T_point_coordinates_y = new double[7];
        double[] A;
        double[] rough_coordinate_x;
        double[] rough_coordinate_y;
        Matrix<double> B_matrix;
        Matrix<double> R;
        Matrix<double> P_matrix;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 数据预处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Original_side_length.Length==0)
            {
                Console.WriteLine("请先进行数据读取！");
            }
            else
            {
                float[] floatOriginal_side_length = new float[Original_side_length.Length];
                for (int l = 0; l < Original_side_length.Length; l++)
                {
                    if (float.TryParse(Original_side_length[l], out float result))
                    {
                        floatOriginal_side_length[l] = result;
                    }
                    else
                    {
                        // 如果无法成功转换，则在这里处理错误或采取其他措施。
                        Console.WriteLine($"无法将字符串 \"{Original_side_length[l]}\" 转换为浮点数。");
                    }
                }
                float[] floatOriginal_plane_angle = new float[Original_plane_angle.Length];
                for (int l = 0; l < Original_plane_angle.Length; l++)
                {
                    if (float.TryParse(Original_plane_angle[l], out float result))
                    {
                        floatOriginal_plane_angle[l] = result;
                    }
                    else
                    {
                        // 如果无法成功转换，则在这里处理错误或采取其他措施。
                        Console.WriteLine($"无法将字符串 \"{Original_plane_angle[l]}\" 转换为浮点数。");
                    }
                }
                float[] floatStarting_point_coordinates_x = new float[starting_point_coordinates_x.Length];
                for (int l = 0; l < starting_point_coordinates_x.Length; l++)
                {
                    if (float.TryParse(starting_point_coordinates_x[l], out float result))
                    {
                        floatStarting_point_coordinates_x[l] = result;
                    }
                    else
                    {
                        // 如果无法成功转换，则在这里处理错误或采取其他措施。
                        Console.WriteLine($"无法将字符串 \"{starting_point_coordinates_x[l]}\" 转换为浮点数。");
                    }
                }
                float[] floatStarting_point_coordinates_y = new float[starting_point_coordinates_y.Length];
                for (int l = 0; l < starting_point_coordinates_y.Length; l++)
                {
                    if (float.TryParse(starting_point_coordinates_y[l], out float result))
                    {
                        floatStarting_point_coordinates_y[l] = result;
                    }
                    else
                    {
                        // 如果无法成功转换，则在这里处理错误或采取其他措施。
                        Console.WriteLine($"无法将字符串 \"{starting_point_coordinates_y[l]}\" 转换为浮点数。");
                    }
                }
                Control_network_coordinate_estimation.Coordinate_estimate(floatOriginal_plane_angle, floatOriginal_side_length, floatStarting_point_coordinates_x, floatStarting_point_coordinates_y, out rough_coordinate_x, out rough_coordinate_y, out A);
                for (int i = 0; i < rough_coordinate_x.Length; i++)
                {
                    if (i < dataGridView1.Rows.Count)
                    {
                        // 将数据添加到第三列
                        dataGridView1.Rows[i].Cells[2].Value = rough_coordinate_x[i];

                        // 将数据添加到第四列
                        dataGridView1.Rows[i].Cells[3].Value = rough_coordinate_y[i];
                    }
                    else
                    {
                        // 如果DataGridView的行数不足，添加新行并继续添加数据
                        dataGridView1.Rows.Add(rough_coordinate_x[i], rough_coordinate_x[i]);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void 数据导入ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/控制网平差设计/原始数据.txt", Encoding.UTF8))
                {
                    string line;
                    int i = 0;
                    int j = 0;
                    int k = 0;

                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        if(!string.IsNullOrWhiteSpace(line))
                        {
                            if(j==0)
                            {
                                string[] columns = line.Split(',');
                                Original_side_length[i] = columns[0];
                                Original_plane_angle[i] = columns[1];
                                i++;

                                // 将列数据添加到 DataGridView 中
                                dataGridView1.Rows.Add(columns);
                            }
                            else
                            {
                                if(k==0)
                                {
                                    starting_point_coordinates_x = line.Split(',');
                                    k++;
                                }
                                else
                                {
                                    starting_point_coordinates_y = line.Split(',');
                                }
                            }
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            catch (Exception h)
            {
                // 向用户显示出错消息
                Console.WriteLine("文件读取错误");
                Console.WriteLine(h.Message);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void 控制网平差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[,] Angle_adjustment_equation_parameters;
            double[,] Side_length_adjustment_equation_parameters;
            float[] floatOriginal_side_length = new float[Original_side_length.Length];
            int k = 0;
            for (int l = 0; l < Original_side_length.Length; l++)
            {
                if (float.TryParse(Original_side_length[l], out float result))
                {
                    floatOriginal_side_length[l] = result;
                }
                else
                {
                    // 如果无法成功转换，则在这里处理错误或采取其他措施。
                    Console.WriteLine($"无法将字符串 \"{Original_side_length[l]}\" 转换为浮点数。");
                }
            }
            float[] floatOriginal_plane_angle = new float[Original_plane_angle.Length];
            for (int l = 0; l < Original_plane_angle.Length; l++)
            {
                if (float.TryParse(Original_plane_angle[l], out float result))
                {
                    floatOriginal_plane_angle[l] = result;
                }
                else
                {
                    // 如果无法成功转换，则在这里处理错误或采取其他措施。
                    Console.WriteLine($"无法将字符串 \"{Original_plane_angle[l]}\" 转换为浮点数。");
                }
            }
            Adjustment_equation_parameters.Adjustment_equation_parameter_calculation(rough_coordinate_x, rough_coordinate_y, floatOriginal_side_length, floatOriginal_plane_angle, A, out Angle_adjustment_equation_parameters, out Side_length_adjustment_equation_parameters);
            for(int i=0;i<13;i++)
            {
                if(i == 0)
                {
                    for(int j=0;j<10;j++)
                    {
                        if(j+4>5)
                        {
                            B[i, j] = 0;
                        }
                        else
                        {
                            B[i, j] = Angle_adjustment_equation_parameters[i, j + 4];
                        }
                    }
                }
                if(i == 1)
                {
                    for( int j=0;j<10; j++)
                    {
                        if(j+2>5)
                        {
                            B[i,j] = 0;
                        }
                        else
                        {
                            B[i, j] = Angle_adjustment_equation_parameters[i, j + 2];
                        }
                    }
                }
                if(i>1&&i<5)
                {
                    for(int j = 0;j<10;j++)
                    {
                        if(j>2*k-1&&j<6+2*k)
                        {
                            B[i, j] = Angle_adjustment_equation_parameters[i, j-2*k];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                    k++;
                }
                k = 0;
                if(i == 5)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j>5)
                        {
                            B[i, j] = Angle_adjustment_equation_parameters[i, j-6];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                }
                if(i == 6)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j>7)
                        {
                            B[i, j] = Angle_adjustment_equation_parameters[i, j - 8];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                }
                if(i == 7)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j<2)
                        {
                            B[i, j] = Side_length_adjustment_equation_parameters[i-7, j + 2];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                }
                if(i>7&&i<12)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j>2*k-1&&j<4+2*k)
                        {
                            B[i, j] = Side_length_adjustment_equation_parameters[i-7, j-2*k];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                    k++;
                }
                else if(i==12)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j>7)
                        {
                            B[i, j] = Side_length_adjustment_equation_parameters[i-7, j - 8];
                        }
                        else
                        {
                            B[i, j] = 0;
                        }
                    }
                }
            }
            for(int i = 0;i < 13;i++)
            {
                if(i<7)
                {
                    l[i, 0] = Angle_adjustment_equation_parameters[i, 6];
                }
                else
                {
                    l[i, 0] = Side_length_adjustment_equation_parameters[i-7, 4];
                }
            }
            B_matrix = Matrix<double>.Build.DenseOfArray(B);
            double[,] Weight_Matrix = Weight_matrix.Weight_matrix_calculation(floatOriginal_side_length, floatOriginal_plane_angle);
            P_matrix = Matrix<double>.Build.DenseOfArray(Weight_Matrix);
            Matrix<double> l_matrix = Matrix<double>.Build.DenseOfArray(l);
            Matrix<Complex> Complex_B_matrix = Matrix<Complex>.Build.Dense(B_matrix.RowCount, B_matrix.ColumnCount);
            for (int i = 0; i < B_matrix.RowCount; i++)
            {
                for (int j = 0; j < B_matrix.ColumnCount; j++)
                {
                    Complex_B_matrix[i, j] = new Complex(B_matrix[i, j], 0.0);
                }
            }
            Matrix<Complex> Transposed_Complex_B_matrix = Complex_B_matrix.Transpose();
            Matrix<double> ComplexToDouble = Transposed_Complex_B_matrix.Map(c => c.Real);

            // 计算乘积矩阵
            Matrix<double> product = ComplexToDouble.Multiply(P_matrix).Multiply(B_matrix);

            // 计算伪逆矩阵
            Matrix<double> inverse = product.PseudoInverse();

            // 计算 Parametric_matrix
            Matrix<double> Parametric_matrix = inverse.Multiply(ComplexToDouble).Multiply(P_matrix).Multiply(l_matrix);
            R = B_matrix * Parametric_matrix- l_matrix;
            for(int i = 0;i<13; i++)
            {
                V[i] = R[i,0];
                if(i < 7)
                {
                    V_plane_angle[i] = V[i];
                }
                else
                {
                    V_side_length[i-7] = V[i];
                }
            }
            for(int i = 0; i < 6; i++)
            {
                T_side_length[i] = floatOriginal_side_length[i] + V_side_length[i];
            }
            for (int i = 0; i < 7; i++)
            {
                T_plane_angle[i] = floatOriginal_plane_angle[i] + V_plane_angle[i];
            }
            for (int i = 0; i < 6; i++)
            {
                if (i < dataGridView1.Rows.Count)
                {
                    // 将数据添加到第五列
                    dataGridView1.Rows[i].Cells[4].Value = T_side_length[i];
                }
                else
                {
                    // 如果DataGridView的行数不足，添加新行并继续添加数据
                    dataGridView1.Rows.Add(T_side_length[i]);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (i < dataGridView1.Rows.Count)
                {
                    // 将数据添加到第六列
                    dataGridView1.Rows[i].Cells[5].Value = T_plane_angle[i];
                }
                else
                {
                    // 如果DataGridView的行数不足，添加新行并继续添加数据
                    dataGridView1.Rows.Add(T_plane_angle[i]);
                }
            }
            for(int i = 0;i < 10;i++)
            {
                V_x[i] = Parametric_matrix[i, 0]/1000;
            }
            T_point_coordinates_x[0] = double.Parse(starting_point_coordinates_x[0]);
            T_point_coordinates_y[0] = double.Parse(starting_point_coordinates_y[0]);
            T_point_coordinates_x[1] = double.Parse(starting_point_coordinates_x[1]);
            T_point_coordinates_y[1] = double.Parse(starting_point_coordinates_y[1]);
            for (int i = 0; i < 5; i++)
            {
                T_point_coordinates_x[i + 2] = rough_coordinate_x[i + 2] + V_x[2*i];
            }
            for (int i = 0; i < 5; i++)
            {
                T_point_coordinates_y[i + 2] = rough_coordinate_y[i + 2] + V_x[2 * i+1];
            }
            for (int i = 0; i < 7; i++)
            {
                if (i < dataGridView1.Rows.Count)
                {
                    // 将数据添加到第七列
                    dataGridView1.Rows[i].Cells[6].Value = T_point_coordinates_x[i];

                    // 将数据添加到第八列
                    dataGridView1.Rows[i].Cells[7].Value = T_point_coordinates_y[i];
                }
                else
                {
                    // 如果DataGridView的行数不足，添加新行并继续添加数据
                    dataGridView1.Rows.Add(T_point_coordinates_x[i], T_point_coordinates_y[i]);
                }
            }
        }

        private void 精度评定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Matrix<Complex> Complex_R_matrix = Matrix<Complex>.Build.Dense(R.RowCount, R.ColumnCount);
            for (int i = 0; i < R.RowCount; i++)
            {
                for (int j = 0; j < R.ColumnCount; j++)
                {
                    Complex_R_matrix[i, j] = new Complex(R[i, j], 0.0);
                }
            }
            Matrix<Complex> Transposed_Complex_R_matrix = Complex_R_matrix.Transpose();
            Matrix<double> ComplexToDouble = Transposed_Complex_R_matrix.Map(c => c.Real);
            Matrix<double> product = ComplexToDouble.Multiply(P_matrix).Multiply(R);
            double result = product[0, 0];
            double Delta0 = Math.Sqrt(result / (Original_plane_angle.Length + Original_side_length.Length - (rough_coordinate_x.Length * 2 - 4)));
            Matrix<Complex> Complex_B_matrix = Matrix<Complex>.Build.Dense(B_matrix.RowCount, B_matrix.ColumnCount);
            for (int i = 0; i < B_matrix.RowCount; i++)
            {
                for (int j = 0; j < B_matrix.ColumnCount; j++)
                {
                    Complex_B_matrix[i, j] = new Complex(B_matrix[i, j], 0.0);
                }
            }
            Matrix<Complex> Transposed_Complex_B_matrix = Complex_B_matrix.Transpose();
            Matrix<double> ComplexToDouble1 = Transposed_Complex_B_matrix.Map(c => c.Real);

            // 计算乘积矩阵
            Matrix<double> product1 = ComplexToDouble.Multiply(P_matrix).Multiply(B_matrix);
            Matrix<double> inverse = product1.PseudoInverse();
            decimal[] Delta_x = new decimal[5];
            decimal[] Delta_y = new decimal[5];
            decimal[] middle = new decimal[10];
            decimal middle1;
            middle1 = (decimal)Delta0;
            for (int i = 0; i < 10; i++)
            {
                middle[i] = (decimal)inverse[i, 0];
            }
            for (int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    if(i == j)
                    {
                        if(i%2 == 0)
                        {
                            Delta_x[i/2] = middle1 * Math.Sqrt(middle[i]);
                            Console.WriteLine(Math.Sqrt(middle[i]));
                        }
                        else if(i%2 != 0)
                        {
                            Delta_y[(i-1)/2] = middle1 * Math.Sqrt(inverse[i, 0]);
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (i < dataGridView1.Rows.Count)
                {
                    // 将数据添加到第九列
                    dataGridView1.Rows[i].Cells[8].Value = Delta_x[i];

                    // 将数据添加到第十列
                    dataGridView1.Rows[i].Cells[9].Value = Delta_y[i];
                }
                else
                {
                    // 如果DataGridView的行数不足，添加新行并继续添加数据
                    dataGridView1.Rows.Add(Delta_x[i], Delta_y[i]);
                }
            }
        }
    }
}
