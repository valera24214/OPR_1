using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[,] Matr = new int[7, 7]
            {
                {int.MaxValue, 13, 14, 6, 8, 4, 12 },
                {12, int.MaxValue, 12, 16, 15, 7, 7 },
                {11, 10, int.MaxValue, 8, 8, 5, 17 },
                {8, 8, 5, int.MaxValue, 10, 9, 8 },
                {6, 8, 9, 7, int.MaxValue, 4, 9 },
                {7, 6, 15, 14, 16, int.MaxValue, 10 },
                {9, 14, 8, 15, 7, 12, int.MaxValue },
            };

        int count;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out count))
            {
                dataGridView1.RowCount = count;
                dataGridView1.ColumnCount = count;;
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (i == j)
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView1.Rows[i].Cells[j].Value = double.PositiveInfinity;
                        }
                        dataGridView1.Columns[i].HeaderCell.Value = "i" + (i + 1);
                        dataGridView1.Rows[i].HeaderCell.Value = "j" + (i + 1);
                    }
                }
            }   
            else
                MessageBox.Show("Введите корректное значение");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        if (i != j)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = Matr[i, j];
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Создайте необходимую матрицу!");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[,] m = new int[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (int.TryParse(dataGridView1.Rows[i].Cells[j].Value.ToString(), out int res))
                    {
                        m[i, j] = res;
                    }
                    else
                        m[i, j] = int.MaxValue;
                }
            }
            Matrix matrix = new Matrix(m, count);
            m = matrix.Count();
            count = 2;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i != j)
                        dataGridView1.Rows[i].Cells[j].Value = m[i, j];
                }
            }
        }
    }
}
