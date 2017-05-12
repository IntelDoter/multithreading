using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace matrixMultiplication
{
    class Program
    {
        public static int[,] matrixC;
        public static int[,] matrixB;
        public static int[,] matrixA;

        public static int[,] fillMatrix(int width, int height, Random rnd)
        {
            int[,] matrix = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = rnd.Next(0, 10);
                }
            }
            return matrix;
        }

        public static void logMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void writeMatrix(int[,] matrix, StreamWriter writer)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    writer.Write(matrix[i, j] + " ");
                }
                writer.WriteLine();
            }
            writer.WriteLine();
        }

        public static void matrixMultiplication()
        {
            Dictionary<string, int> myObj = new Dictionary<string, int>(2);
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    /*for (int k = 0; k < matrixB.GetLength(0); k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }*/
                    myObj.Add("rowA", i);
                    myObj.Add("colB", j);
                    multiplicateNumb(myObj);
                    myObj.Clear();
                }
            }
        }

        public static int[] getCol(int col)
        {
            int arrLen = matrixA.GetLength(0);
            int[] newArr = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                newArr[i] = matrixA[col, i];
            }
            return newArr;
        }

        public static int[] getRow(int row)
        {
            int arrLen = matrixB.GetLength(1);
            int[] newArr = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                newArr[i] = matrixB[i, row];
            }
            return newArr;
        }

        public static void multiplicateNumb(object obj)
        {
            Dictionary<string, int> myObj = (Dictionary<string, int>)obj;
            int resRow = myObj["rowA"];
            int resCol = myObj["colB"];

            int[] row = getRow(resRow);
            int[] col = getCol(resCol);

            for (int i = 0; i < row.Length; i++)
            {
               matrixC[resRow, resCol] += row[i] * col[i];
            }

        }

        public static void matrixMultiplicationWithThreads()
        {
            
        }

        static void Main(string[] args)
        {
            bool riba = false;
            int matrixBheight = 2;
            int matrixAwidth = 2;
            int matrixAheight = 2;
            int matrixBwidth = 2;

            if (!riba)
            { 
                Console.WriteLine("Введите количество строк матрицы А");
                matrixAwidth = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество столбцов матрицы А");
                matrixAheight = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество строк матрицы Б");
                matrixBwidth = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество столбцов матрицы Б");
                matrixBheight = int.Parse(Console.ReadLine());
            }

            matrixC = new int[matrixAwidth, matrixBheight];

            Random rnd = new Random();
            matrixA = fillMatrix(matrixAwidth, matrixAheight, rnd);
            matrixB = fillMatrix(matrixBwidth, matrixBheight, rnd);
            matrixMultiplication();

            FileStream file = new FileStream("C://out/out.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(file);
            writeMatrix(matrixA, writer);
            writeMatrix(matrixB, writer);
            writeMatrix(matrixC, writer);
            writer.Close();
            file.Close();


            logMatrix(matrixA);
            logMatrix(matrixB);
            logMatrix(matrixC);

            Console.ReadLine();
        }
    }
}
