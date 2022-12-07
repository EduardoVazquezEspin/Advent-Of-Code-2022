using System;
using System.Collections;

namespace AdventOfCode
{
    public class Pair
    {
        public int min;
        public int max;

        public Pair(string min, string max)
        {
            this.min = Convert.ToInt32(min);
            this.max = Convert.ToInt32(max);
        }

        public bool IsIncluded(Pair other)
        {
            if (this.min >= other.min && this.max <= other.max) return true;
            return false;
        }

        public bool DoOverlap(Pair other)
        {
            return this.min <= other.min && this.max >= other.min || this.min <= other.max && this.max >= other.max || this.IsIncluded(other);
        }
    }
    
    public class Day4
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(4);
            //Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            int val = 0;
            for (int i=0; i < data.Length - 1; i++)
            {
                string[] dataSplit = data[i].Split(',');
                string[] first = dataSplit[0].Split('-');
                string[] second = dataSplit[1].Split('-');
                Pair one = new Pair(first[0], first[1]);
                Pair two = new Pair(second[0], second[1]);
                val += (one.IsIncluded(two) || two.IsIncluded(one) ? 1 : 0);
            }
            Console.WriteLine(val);
        }

        public static void Part2(string[] data)
        {
            int val = 0;
            for (int i=0; i < data.Length - 1; i++)
            {
                string[] dataSplit = data[i].Split(',');
                string[] first = dataSplit[0].Split('-');
                string[] second = dataSplit[1].Split('-');
                Pair one = new Pair(first[0], first[1]);
                Pair two = new Pair(second[0], second[1]);
                val += (one.DoOverlap(two) ? 1 : 0);
            }
            Console.WriteLine(val);
        }
    }
}