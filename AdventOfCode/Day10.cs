using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AdventOfCode
{
    public class Day10
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(10);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            int[] times = new[] {20, 60, 100, 140, 180, 220};
            int dataPosition = 0;
            int cycle = 1;
            int X = 1;
            int val = 0;
            bool repeat = false;
            while (dataPosition != data.Length - 1)
            {

                if (times.Contains(cycle))
                {
                    val += X * cycle;
                }

                if (data[dataPosition] != "noop")
                    repeat = ! repeat;

                if (!repeat)
                {
                    if(data[dataPosition] != "noop")
                        X += Convert.ToInt32(data[dataPosition].Split(' ')[1]);
                    dataPosition++;
                }
                
                cycle++;
            }
            Console.WriteLine(val);
        }
        
        public static void Part2(string[] data)
        {
            int dataPosition = 0;
            int cycle = 1;
            int X = 1;
            bool repeat = false;
            while (dataPosition != data.Length - 1)
            {
                Console.Write(Math.Abs(cycle%40-1-X) <=1 ? '#' : '.');
                if(cycle%40 == 0)
                    Console.Write("\n");

                if (data[dataPosition] != "noop")
                    repeat = ! repeat;

                if (!repeat)
                {
                    if(data[dataPosition] != "noop")
                        X += Convert.ToInt32(data[dataPosition].Split(' ')[1]);
                    dataPosition++;
                }
                
                cycle++;
            }
        }

    }
}