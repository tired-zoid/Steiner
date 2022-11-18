using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steiner
{
    public partial class Form1 : Form
    {
        int[,] usedE;
        Random rnd = new Random();
        int dimX;
        public int topQ = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
             topQ = Convert.ToInt32(numericUpDown2.Value);
            dimX = Convert.ToInt32(numericUpDown1.Value);
            int[,] matRas = new int[topQ, topQ];
            int[,] elCoord = new int[2, topQ];
            Placement(ref matRas, elCoord,  topQ, dimX);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            for (int i = 0; i < matRas.GetLength(0); i++)
            {
                dataGridView1.Columns.Add("Column1", "X");
                dataGridView1.Rows.Add();
            }
            for (int i = 0; i < matRas.GetLength(0); i++)
            {
                for (int j = 0; j < matRas.GetLength(0); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matRas[i, j];
                }
            }


            for (int i = 0; i < elCoord.GetLength(1); i++)
            {
                dataGridView3.Columns.Add("Column1", "T");
                dataGridView3.Rows.Add();
            }
            for (int i = 0; i < elCoord.GetLength(0); i++)
            {
                for (int j = 0; j < elCoord.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = elCoord[i, j];
                }
            }
            int length = 0;
            int[,] usedE = new int[2, topQ];
            int no_edge;
            bool[] selected = new bool[topQ];
            no_edge = 0;
            selected[0] = true;
            int x;
            int y;
             // Prima 
            while (no_edge < topQ - 1)
            {
                int min = 9999;
                x = 0;
                y = 0;

                for (int i = 0; i < topQ; i++)
                {
                    if (selected[i])
                    {
                        for (int j = 0; j < topQ; j++)
                        {
                            if (!selected[j])
                            {
                                if (matRas[i, j] < min)
                                {
                                    min = matRas[i, j];
                                    x = i;
                                    y = j;
                                }

                            }
                        }
                    }
                }
                usedE[0, no_edge] = x;
                usedE[1, no_edge] = y;
                length += matRas[x, y];
                selected[y] = true;
                no_edge++;
            }


            int[,] firstEdge = new int[2, dimX*dimX];

            for (int i = 0; i < firstEdge.GetLength(0); i++)
            {
                for (int j = 0; j < firstEdge.GetLength(1); j++)
                {
                    firstEdge[i, j] = 0;
                }
            }
            int[,] secondEdge = new int[2, dimX*dimX];

            for (int i = 0; i < firstEdge.GetLength(0); i++)
            {
                for (int j = 0; j < firstEdge.GetLength(1); j++)
                {
                    firstEdge[i, j] = 0;                }
            }

            int[,] ST = new int[2, dimX * dimX];

            for (int i = 0; i < ST.GetLength(0); i++)
            {
                for (int j = 0; j < ST.GetLength(1); j++)
                {
                    ST[i, j] = 0;
                }
            }

            int g = 0;
            int h = 0;
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int m = 0;
            int n = 0;


            while (m != topQ - 1)
            {
                g = usedE[0, m];
                h = usedE[1, m];
                x1 = elCoord[0, g];
                y1 = elCoord[1, g];
                x2 = elCoord[0, h];
                y2 = elCoord[1, h];
                int p = 0;
                int q = 0;

                x1 = elCoord[0, g];
                y1 = elCoord[1, g];
                x2 = elCoord[0, h];
                y2 = elCoord[1, h];

                // первая конфигурация 
                while (x1 != x2 && y1 != y2) 
                {

                    while (x1 != x2)
                    {
                        if (x1 <= x2)
                        {
                            firstEdge[0, p] = x1;
                            firstEdge[1, p] = y1;
                            x1++;
                            p++;
                        }
                        else
                        {
                            firstEdge[0, p] = x1;
                            firstEdge[1, p] = y1;
                            x1--;
                            p++;
                        }
                    }

                    while (y2 != y1)
                    {

                        if (y2 <= y1)
                        {
                            firstEdge[0, p] = x2;
                            firstEdge[1, p] = y2;
                            y2++;
                            p++;
                        }
                        else
                        {
                            firstEdge[0, p] = x2;
                            firstEdge[1, p] = y2;
                            y2--;
                            p++;
                        }
                    }

                }

                x1 = elCoord[0, g];
                y1 = elCoord[1, g];
                x2 = elCoord[0, h];
                y2 = elCoord[1, h];

                // вторая конфигурация 
                while (x1 != x2 && y1 != y2) 
                {

                    while (y1 != y2)
                    {
                        if (y1 <= y2)
                        {
                            secondEdge[0, q] = x1;
                            secondEdge[1, q] = y1;
                            y1++;
                            q++;
                        }
                        else
                        {
                            secondEdge[0, q] = x1;
                            secondEdge[1, q] = y1;
                            y1--;
                            q++;
                        }
                    }

                    while (x2 != x1)
                    {

                        if (x2 <= x1)
                        {
                            secondEdge[0, q] = x2;
                            secondEdge[1, q] = y2;
                            x2++;
                            q++;
                        }
                        else
                        {
                            secondEdge[0, q] = x2;
                            secondEdge[1, q] = y2;
                            x2--;
                            q++;
                        }
                    }

                }

                ST[0, 0] = elCoord[0,0]; // добавляем координаты первой точки в дерево Штейнера
                ST[1, 0] = elCoord[1,0];       

                int[,] points1 = new int[2, dimX * dimX]; // сравниваем первую конфигурацию 
                int k = 0;
                for (int i = 0; i < firstEdge.GetLength(0); i++)
                {
                    for (int j = 0; j < firstEdge.GetLength(1); j++)
                    {
                        if (ST[i, j] > 0)
                        {
                            if (firstEdge[i, j] == ST[i, j])
                            {
                                points1[0, k] = i;
                                points1[1, k] = j;
                                k++;
                            }

                        }

                    }
                }

                int[,] points2 = new int[2, dimX * dimX]; // сравниваем вторую конфигурацию с деревом 
                int z = 0;
                for (int i = 0; i < secondEdge.GetLength(0); i++)
                {
                    for (int j = 0; j < secondEdge.GetLength(1); j++)
                    {
                        if (ST[i, j] > 0)
                        {
                            if (secondEdge[i, j] == ST[i, j])
                            {
                                points2[0, z] = i;
                                points2[1, z] = j;
                                z++;
                            }
                        }

                    }
                }

                int numP1 = 0; // подсчет ненулевых элементов совпадение первого ребра с деревом 
                for (int i = 0; i < points1.GetLength(0); i++) 
                {
                    for (int j = 0; j < points1.GetLength(1); j++)
                    {
                        if ((points1[0, j]  != 0) && (points1[1, j] !=0))
                        {
                            numP1++;
                        }

                    }
                }

                int numP2 = 0; // подсчет ненулевых элементов совпадений второго ребра с деревом 
                for (int i = 0; i < points2.GetLength(0); i++) 
                {
                    for (int j = 0; j < points2.GetLength(1); j++)
                    {
                        if ((points2[0, j] != 0) && (points2[1, j] != 0))
                        {
                            numP2++;
                        }

                    }
                }


                int l = 0;   // добавление первой конфигурации в дерево
                if (numP1 >= numP2)
                {
                    while ((firstEdge[0,l] > 0) || (firstEdge[1, l] > 0))  
                    {
                        for (int i = 0; i < ST.GetLength(1); i++)
                        {
                            if (ST[0, i] == 0 && ST[1, i] == 0)
                            {
                                ST[0, i] = firstEdge[0, l];
                                ST[1, i] = firstEdge[1, l];
                                l++;
                            }
                        }
                    }
                }
                            // добавление второй конфигурации в дерево
                else
                {
                    while ((secondEdge[0, l] > 0) || (secondEdge[1, l] > 0))
                    {

                            for (int i = 0; i < ST.GetLength(1); i++)
                            {
                                if (ST[0, i] == 0 && ST[1, i] == 0)
                                {
                                    ST[0, i] = secondEdge[0, l];
                                    ST[1, i] = secondEdge[1, l];
                                    l++;
                                }
                            }
                        
                    }
                }
                    
            m++;
            }



            int[,] lastST = new int[2, dimX * dimX];
            lastST[0, 0] = ST[0, 0];
            int b = 0;
            for (int i = 0; i < ST.GetLength(1); i++)
            {

                if (lastST[0,i] == ST[0,b])
                {
                    if (lastST[1,i] == ST[1,b])
                    {
                        b++;
                    }
                    else
                    {
                        lastST[0, i] = ST[0, b];
                        lastST[1, i] = ST[1, b];
                        b++;
                    }
                }
                else
                {
                    lastST[0, i] = ST[0, b];
                    lastST[1, i] = ST[1, b];
                    b++;
                }

            }

                int pointsST = 0;

                for (int j = 0; j < lastST.GetLength(1); j++)
                {
                    if ((lastST[0, j] != 0) && (lastST[1, j] != 0))
                    {
                        pointsST++;
                    }

                }


            textBox1.Text = pointsST.ToString();
            textBox2.Text = length.ToString();
            dataGridView2.Columns.Add("Column1", "X");
            dataGridView2.Columns.Add("Column2", "Y");
            for (int i = 0; i < usedE.GetLength(1) - 1; i++)
            {
                dataGridView2.Rows.Add();
            }

            for (int i = 0; i < usedE.GetLength(1) - 1; i++)
            {
                for (int j = 0; j < usedE.GetLength(0); j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = usedE[j, i] + 1;
                }
            }
        }

        public (int[,], int [,]) Placement(ref int[,] matRas, int [,] elCoord, int topQ, int dimX)
        {
            for (int i = 0; i < elCoord.GetLength(0); i++)
            {
                for (int j = 0; j < elCoord.GetLength(1); j++)
                {
                    elCoord[i, j] = rnd.Next(dimX);
                }
            }

            int a = 0;
            int b = 0;

            for (int x = 0; x < matRas.GetLength(0); x++)
            {
                for (int y = 0; y < matRas.GetLength(0); y++)
                {
                    // евклидово расстояние 
                    int dX = elCoord[0, a] - elCoord[0, b];
                    int dY = elCoord[1, a] - elCoord[1, b];
                    double distance = Math.Sqrt(dX * dX + dY * dY);
                    matRas[x, y] = Convert.ToInt32(distance);
                    dX = 0;
                    dY = 0;
                    b++;
                }
                b = 0;
                a++;
            }
            return (matRas, elCoord);
        }
    }
}

