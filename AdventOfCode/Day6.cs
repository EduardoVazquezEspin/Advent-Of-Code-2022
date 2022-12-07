using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day6
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(6);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            List<char> last3chars = new List<char>();
            int i = 0;
            for (; i < data[0].Length && last3chars.Count != 4; i++)
            {
                if(!last3chars.Contains(data[0][i]))
                    last3chars.Add(data[0][i]);
                else
                {
                    while (last3chars.Contains(data[0][i]))
                    {
                        last3chars.RemoveAt(0);
                    }
                    last3chars.Add(data[0][i]);
                }
            }
            Console.WriteLine(i);
        }

        public static void Part2(string[] data)
        {
            List<char> last3chars = new List<char>();
            int i = 0;
            for (; i < data[0].Length && last3chars.Count != 14; i++)
            {
                if(!last3chars.Contains(data[0][i]))
                    last3chars.Add(data[0][i]);
                else
                {
                    while (last3chars.Contains(data[0][i]))
                    {
                        last3chars.RemoveAt(0);
                    }
                    last3chars.Add(data[0][i]);
                }
            }
            Console.WriteLine(i);
        }
    }
}