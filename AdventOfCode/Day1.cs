using System;
using System.IO;

namespace AdventOfCode
{
    internal class Day1
    {
        public static void Solution(string[] args)
        {
            int max = -1;
            int[] top3 = new int[4];
            int current = 0;
            string[] lines = File.ReadAllLines("/home/eduard/Escriptori/Deures/AdventOfCode/AdventOfCode1/dades.txt");
            for(int i =0 ; i< lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    current += Convert.ToInt32(lines[i]);
                }
                else
                {
                    if (i < 4)
                        top3[i] = current;
                    else
                    {
                        Array.Sort(top3);
                        top3[0] = current;
                    }
                    current = 0;
                }
            }
            Array.Sort(top3);
            Console.WriteLine(top3[1] + " || " + top3[2] + " || " + top3[3] + " || " + (top3[1]+top3[2]+top3[3]));
        }
    }
}