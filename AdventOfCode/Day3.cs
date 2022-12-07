using System;

namespace AdventOfCode
{
    internal class Day3
    {
        public static void Solution(string[] args)
        {
            string[] data = InData.ReadTxtFile(3);
            //Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length-1; i++)
            {
                char repeated = GetRepeatedChar(data[i]);
                int val = GetPriority(repeated);
                if (val == 0)
                    Console.WriteLine("Oups, something went wrong");
                else
                    sum += val;
            }
            Console.WriteLine(sum);
        }
        
        public static void Part2(string[] data)
        {
            int sum = 0;
            for (int i = 0; i < (data.Length-1)/3; i++)
            {
                string firstElf = data[3 * i];
                string secondElf = data[3 * i + 1];
                string thirdElf = data[3 * i + 2];
                char common = GetCommon(firstElf, secondElf, thirdElf);
                int val = GetPriority(common);
                if (val == 0)
                    Console.WriteLine("Oups, something went wrong");
                else
                    sum += val;
            }
            Console.WriteLine(sum);
        }

        static int GetPriority(char c)
        {
            if (c >= 'a' && c <= 'z')
                return c - 'a' + 1;
            if (c >= 'A' && c <= 'Z')
                return c - 'A' + 27;
            return 0;
        }

        static char GetRepeatedChar(string s)
        {
            string lower = s.Substring(0, s.Length / 2);
            string upper = s.Substring(s.Length/2, s.Length / 2);
            for (int i = 0; i < lower.Length; i++)
            {
                if (upper.Contains(lower[i].ToString()))
                    return lower[i];
            }
            return '*';
        }

        static char GetCommon(string first, string second, string third)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (second.Contains(first[i].ToString()) && third.Contains(first[i].ToString()))
                    return first[i];
            }
            return '*';
        }
    }
}