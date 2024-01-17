using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Matrix
    {
        private int[,] matrix;
        private int count;
        int rez;

        int[] vert_min;
        int[] hor_min;

        int[,] new_matrix_left;
        int[,] new_matrix_right;

        public Matrix(int[,] matrix, int count)
        {
            this.count = count;
            this.matrix = new int[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        public void Step1(int[,] matrix, string str, int count)
        {
            switch (str)
            {
                case "vert":
                    {
                        vert_min = new int[count];
                        for (int i = 0; i < count; i++)
                        {
                            vert_min[i] = matrix[i, 0];
                            for (int j = 0; j < count; j++)
                            {
                                if (matrix[i, j] < vert_min[i])
                                    vert_min[i] = matrix[i, j];
                            }
                        }
                        break;
                    }

                case "hor":
                    {
                        hor_min = new int[count];
                        for (int j = 0; j < count; j++)
                        {
                            hor_min[j] = matrix[0, j];
                            for (int i = 0; i < count; i++)
                            {
                                if (matrix[i, j] < hor_min[j])
                                    hor_min[j] = matrix[i, j];
                            }
                        }

                        break;
                    }

                default:
                    {
                        throw new Exception();
                        break;
                    }

            }

            
        }

        public void Step2(int[,] matrix, string str, int count)
        {
            switch (str)
            {
                case "vert":
                    {
                        for (int i = 0; i < count; i++)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                matrix[i, j] -= vert_min[i];
                                if (matrix[i, j] < 0)
                                    matrix[i, j] = 0;
                            }
                        }

                        break;
                    }

                case "hor":
                    {
                        for (int j = 0; j < count; j++)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                matrix[i, j] -= hor_min[j];
                                if (matrix[i, j] < 0)
                                    matrix[i, j] = 0;
                            }
                        }

                        break;
                    }
            }
        }

        private int Min_sum(int r, int c)
        {
            int min_row = matrix[r, 0];
            int min_col = matrix[0, c];

            for (int j = 0; j< count; j++)
            {
                if (matrix[r, j] < min_row && j!=c)
                    min_row = matrix[r, j];
            }

            for (int i = 0; i < count; i++)
            {
                if (matrix[i, c] < min_col && i != r)
                    min_col = matrix[i, c];
            }

            return min_col + min_row;
        }

        private Zero Find_element()
        {
            List<Zero> zeros = new List<Zero>();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        Zero zero = new Zero()
                        {
                            i = i,
                            j = j,
                            rez = Min_sum(i, j),
                        };

                        zeros.Add(zero);
                    }
                }
            }

            zeros = zeros.OrderByDescending(z => z.rez).ToList();

            return zeros[0];
        }

        private int Go_Left()
        {
            Zero zero = Find_element();
            new_matrix_left = matrix;
            new_matrix_left[zero.i, zero.j] = int.MaxValue;
            Step1(new_matrix_left, "vert", count);
            Step1(new_matrix_left, "hor", count);
            Step2(new_matrix_left, "vert", count);
            Step2(new_matrix_left, "hor", count);

            int new_rez = new int();

            for (int i = 0; i < count; i++)
            {
                new_rez += vert_min[i] + hor_min[i];
            }

            return new_rez;
        }

        private int Go_Right()
        {
            Zero zero = Find_element();
            new_matrix_right = new int[count - 1, count - 1];
            for (int i = 0; i < count-1; i++)
            {
                for (int j = 0; j < count-1; j++)
                {
                    if (i < zero.i)
                    {
                        if (j < zero.j)
                        {
                            new_matrix_right[i, j] = matrix[i, j];
                        }
                        else
                        {
                            new_matrix_right[i, j] = matrix[i, j + 1];
                        }
                    }
                    else
                    {
                        if (j < zero.j)
                        {
                            new_matrix_right[i, j] = matrix[i + 1, j];
                        }
                        else
                        {
                            new_matrix_right[i, j] = matrix[i + 1, j + 1];
                        }
                    }

                    if (i == zero.j && j == zero.i)
                    {
                        new_matrix_right[i, j] = int.MaxValue;
                    }
                }
            }

            Step1(new_matrix_right, "vert", count - 1);
            Step1(new_matrix_right, "hor", count - 1);
            Step2(new_matrix_right, "vert", count - 1);
            Step2(new_matrix_right, "hor", count - 1);

            int new_rez = new int();

            for (int i = 0; i < count-1; i++)
            {
                new_rez += vert_min[i] + hor_min[i];
            }

            return new_rez;
        }

        private void Decide()
        {
            Zero zero = Find_element();
            if (Go_Left() < Go_Right())
            {
                matrix[zero.i, zero.j] = int.MaxValue;
                rez += Go_Left();
            }
            else 
            {
                
                matrix = new int[count, count];
                new_matrix_right = new int[count - 1, count - 1];
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1; j++)
                    {
                        if (i < zero.i)
                        {
                            if (j < zero.j)
                            {
                                new_matrix_right[i, j] = matrix[i, j];
                            }
                            else
                            {
                                new_matrix_right[i, j] = matrix[i, j + 1];
                            }
                        }
                        else
                        {
                            if (j < zero.j)
                            {
                                new_matrix_right[i, j] = matrix[i + 1, j];
                            }
                            else
                            {
                                new_matrix_right[i, j] = matrix[i + 1, j + 1];
                            }
                        }
                    }
                }

                count--;

                for (int i = 0; i<count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        matrix[i, j] = new_matrix_right[i, j];
                    }
                }

                rez += Go_Right();
            }
            
        }

        public int[,] Count()
        {
            Step1(matrix, "vert", count);
            Step1(matrix, "hor", count);
            Step2(matrix, "vert", count);
            Step2(matrix, "hor", count);
            for (int i = 0; i < count; i++)
            {
                rez += vert_min[i] + hor_min[i];
            }
            while (count >= 2)
            {
                Decide();
            }
            

            return matrix;
        }
    }
}
