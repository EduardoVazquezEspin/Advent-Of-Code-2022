using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day8
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(8);
            Part1(data);
            Part2(data);
        }

        private static void Part1(string[] data)
        {
            int[,] map = ReadMap(data);
            int cols = data[0].Length;
            int rows = data.Length - 1;
            int[,] maxFromLeft = new int[rows, cols];
            int[,] maxFromRight = new int[rows, cols];
            int[,] maxFromTop = new int[rows, cols];
            int[,] maxFromBot = new int[rows, cols];
            for(int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if (i == 0)
                    maxFromTop[i, j] = -1;
                else
                    maxFromTop[i, j] = Math.Max(map[i-1, j], maxFromTop[i - 1, j]);
                
                if (j == 0)
                    maxFromLeft[i, j] = -1;
                else
                    maxFromLeft[i, j] = Math.Max(map[i, j-1], maxFromLeft[i, j-1]);

            }
            
            for(int i = rows-1; i >= 0; i--)
            for (int j = cols-1; j >= 0; j--)
            {
                if (i == rows-1)
                    maxFromBot[i, j] = -1;
                else
                    maxFromBot[i, j] = Math.Max(map[i+1, j], maxFromBot[i + 1, j]);
                
                if (j == cols-1)
                    maxFromRight[i, j] = -1;
                else
                    maxFromRight[i, j] = Math.Max(map[i, j+1], maxFromRight[i, j+1]);

            }

            int total = 0;
            for(int i =0; i<rows; i++) for(int j =0 ;j<cols; j++)
                if (map[i, j] > Math.Min(Math.Min(maxFromBot[i, j], maxFromRight[i, j]), Math.Min(maxFromLeft[i, j], maxFromTop[i, j])))
                {
                    total++;
                }
            Console.WriteLine(total);
        }

        private static void Part2(string[] data)
        {
            int[,] map = ReadMap(data);
            int cols = data[0].Length;
            int rows = data.Length - 1;
            int maxScenic = -1;
            for(int i =0; i<rows; i++)
            for (int j = 0; j < cols; j++)
            {
                int scenicLeft = 0;
                bool isVisible = true;
                for (int x = 1; i - x >= 0 && isVisible; x++)
                {
                    scenicLeft++;
                    isVisible = map[i, j] > map[i - x, j];
                }
                int scenicRight = 0;
                isVisible = true;
                for (int x = 1; i + x <rows && isVisible; x++)
                {
                    scenicRight++;
                    isVisible = map[i, j] > map[i + x, j];
                }

                int scenicTop = 0;
                isVisible = true;
                for (int x = 1; j-x >= 0 && isVisible; x++)
                {
                    scenicTop++;
                    isVisible = map[i, j] > map[i, j-x];
                }
                int scenicBottom = 0;
                isVisible = true;
                for (int x = 1; j+x < cols && isVisible; x++)
                {
                    scenicBottom++;
                    isVisible = map[i, j] > map[i, j+x];
                }

                if (scenicTop*scenicBottom*scenicLeft*scenicRight > maxScenic)
                {
                    maxScenic = scenicTop*scenicBottom*scenicLeft*scenicRight;
                }
            }
            Console.WriteLine(maxScenic);
        }

        public static int[,] ReadMap(string[] data)
        {
            int cols = data[0].Length;
            int rows = data.Length - 1;
            int[,] map = new int[rows, cols];
            for(int i =0; i<rows; i++)
            for (int j = 0; j < cols; j++)
                map[i, j] = Convert.ToInt32(data[i][j].ToString());
            return map;
        }
    }
}