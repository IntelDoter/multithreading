using System;
using System.Collections.Generic;
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

        public static int[,] fillMatrix(int width, int height)
        {
            Random rnd = new Random();
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

        public static void logMatrix()
        {
            for (int i = 0; i < matrixC.GetLength(0); i++)
            {
                for (int j = 0; j < matrixC.GetLength(1); j++)
                {
                    Console.Write(matrixC[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
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
            int matrixAwidth = 2;
            int matrixAheight = 2;
            int matrixBwidth = 2;
            int matrixBheight = 2;
            matrixC = new int[matrixAwidth, matrixBheight];

            matrixA = fillMatrix(matrixAwidth, matrixAheight);
            matrixB = fillMatrix(matrixBwidth, matrixBheight);
            matrixMultiplication();
            
            logMatrix();
        }
    }
}
