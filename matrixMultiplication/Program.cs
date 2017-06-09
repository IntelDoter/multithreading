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
        public static int[,] threadMatrix;

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

        public static int[,] fillMatrixZero(int width, int height)
        {
            int[,] matrix = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = 0;
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
                    multiplicateNumb(i, j);
                    myObj.Clear();
                }
            }
        }

        public static int[] getCol(int col)
        {
            int arrLen = matrixB.GetLength(1);
            int[] newArr = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                newArr[i] = matrixB[col, i];
            }
            return newArr;
        }

        public static int[] getRow(int row)
        {
            int arrLen = matrixA.GetLength(0);
            int[] newArr = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                newArr[i] = matrixA[i, row];
            }
            return newArr;
        }

        public static void multiplicateNumb(int resRow, int resCol)
        {
            int[] row = getRow(resRow);
            int[] col = getCol(resCol);

            for (int i = 0; i < row.Length; i++)
            {
               matrixC[resRow, resCol] += row[i] * col[i];
            }

        }

        public static void threadFunc()
        {
            int lastIndexI = 0;
            int lastIndexJ = 0;
            for (int i = lastIndexI; i < matrixC.GetLength(0); i++)
            {
                for (int j = lastIndexJ; j < matrixC.GetLength(1); j++)
                {
                    if (matrixC[i, j] == 0)
                    {
                        threadMatrix[i, j] = Thread.CurrentThread.ManagedThreadId;
                        multiplicateNumb(i, j);
                    }
                }
            }
        }

        public static void matrixMultiplicationWithThreads()
        {
            Thread thread1 = new Thread(threadFunc);
            Thread thread2 = new Thread(threadFunc);
            //Thread thread3 = new Thread(threadFunc);
            //Thread thread4 = new Thread(threadFunc);

            thread1.Start();
            thread2.Start();
            //thread3.Start();
            //thread4.Start();

            thread1.Join();
            thread2.Join();
            //thread3.Join();
            //thread4.Join();
        }

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            bool riba = false;
            int matrixBheight = 500;
            int matrixAwidth = 500;
            int matrixAheight = 500;
            int matrixBwidth = 500;

            if (!riba)
            { 
                Console.WriteLine("Введите количество столбцов матрицы А");
                matrixAwidth = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество строк матрицы А");
                matrixAheight = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество столбцов матрицы Б");
                matrixBwidth = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество строк матрицы Б");
                matrixBheight = int.Parse(Console.ReadLine());
            }

            matrixC = new int[matrixAheight, matrixBwidth];

            Random rnd = new Random();
            matrixA = fillMatrix(matrixAwidth, matrixAheight, rnd);
            matrixB = fillMatrix(matrixBwidth, matrixBheight, rnd);
            threadMatrix = fillMatrixZero(matrixAheight, matrixBwidth);
            matrixMultiplicationWithThreads();
            //matrixMultiplication();

            FileStream file = new FileStream("C://out/out.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(file);
            writeMatrix(matrixA, writer);
            writeMatrix(matrixB, writer);
            writeMatrix(matrixC, writer);
            writeMatrix(threadMatrix, writer);
            writer.Close();
            file.Close();

            DateTime end = DateTime.Now;

            //logMatrix(matrixA);
            //logMatrix(matrixB);
            //logMatrix(matrixC);
            //logMatrix(threadMatrix);

            Console.WriteLine(end.Subtract(start).Milliseconds);
            Console.ReadLine();
        }
    }
}
