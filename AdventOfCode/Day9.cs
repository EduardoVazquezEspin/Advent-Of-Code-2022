using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day9
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(9);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            KeyValuePair<int, int> headPosition = new KeyValuePair<int, int>(0, 0);
            KeyValuePair<int, int> tailPosition = new KeyValuePair<int, int>(0, 0);
            List<KeyValuePair<int, int>> visitedPositions = new List<KeyValuePair<int, int>>();
            visitedPositions.Add(tailPosition);
            for (int i = 0; i < data.Length - 1; i++)
            {
                List<KeyValuePair<int,int>> x = GetMovement(data[i]);
                for (int j = 0; j < x.Count; j++)
                {
                    headPosition = new KeyValuePair<int, int>(headPosition.Key + x[j].Key, headPosition.Value + x[j].Value);
                    KeyValuePair<int, int> dif = new KeyValuePair<int, int>(headPosition.Key - tailPosition.Key,
                        headPosition.Value - tailPosition.Value);
                    if (Math.Max(Math.Abs(dif.Key),Math.Abs(dif.Value))>= 2)
                    {
                        KeyValuePair<int, int> movement =
                            new KeyValuePair<int, int>(FunFun(dif.Key), FunFun(dif.Value));
                        tailPosition = new KeyValuePair<int, int>(movement.Key + tailPosition.Key,
                            movement.Value + tailPosition.Value);
                        if (!visitedPositions.Contains(tailPosition))
                            visitedPositions.Add(tailPosition);
                    }
                }
            }
            Console.WriteLine(visitedPositions.Count);
        }
        
        public static void Part2(string[] data)
        {
            KeyValuePair<int, int>[] tailPosition = new KeyValuePair<int, int>[10];
            for (int i = 0; i < 10; i++)
                tailPosition[i] = new KeyValuePair<int, int>(0, 0);
            List<KeyValuePair<int, int>> visitedPositions = new List<KeyValuePair<int, int>>();
            visitedPositions.Add(tailPosition[9]);
            for (int i = 0; i < data.Length - 1; i++)
            {
                List<KeyValuePair<int,int>> x = GetMovement(data[i]);
                for (int j = 0; j < x.Count; j++)
                {
                    tailPosition[0] = new KeyValuePair<int, int>(tailPosition[0].Key + x[j].Key,
                        tailPosition[0].Value + x[j].Value);
                    for (int k = 1; k <= 9; k++)
                    {
                        KeyValuePair<int, int> dif = new KeyValuePair<int, int>(tailPosition[k-1].Key - tailPosition[k].Key,
                            tailPosition[k-1].Value - tailPosition[k].Value);
                        if (Math.Max(Math.Abs(dif.Key), Math.Abs(dif.Value)) >= 2)
                        {
                            KeyValuePair<int, int> movement =
                                new KeyValuePair<int, int>(FunFun(dif.Key), FunFun(dif.Value));
                            tailPosition[k] = new KeyValuePair<int, int>(movement.Key + tailPosition[k].Key,
                                movement.Value + tailPosition[k].Value);
                        }
                    }
                    if (!visitedPositions.Contains(tailPosition[9]))
                        visitedPositions.Add(tailPosition[9]);
                }
            }
            Console.WriteLine(visitedPositions.Count);
        }

        public static List<KeyValuePair<int, int>> GetMovement(string s)
        {
            string[] split = s.Split(' ');
            int value = Convert.ToInt32(split[1]);
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();
            switch (split[0])
            {
                case "U": 
                    for(int i =0; i<value; i++)
                        result.Add(new KeyValuePair<int, int>(0, FunFun(value)));
                    return result;
                case "D":
                    for(int i =0; i<value; i++)
                        result.Add(new KeyValuePair<int, int>(0, - FunFun(value)));
                    return result;
                case "R":
                    for(int i =0; i<value; i++)
                        result.Add(new KeyValuePair<int, int>(FunFun(value), 0));
                    return result;
                case "L":
                    for(int i =0; i<value; i++)
                        result.Add(new KeyValuePair<int, int>(-FunFun(value), 0));
                    return result;
            }

            return result;
        }

        public static int FunFun(int n)
        {
            if (n > 0) return 1;
            if (n < 0) return -1;
            return 0;
        }
    }
}