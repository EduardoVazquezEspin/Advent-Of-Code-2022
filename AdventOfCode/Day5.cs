using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day5
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(5);
            Part1(data);
            Part2(data);
        }

        private static void Part1(string[] data)
        {
            int s;
            int index = 0;
            int numStack = (data[0].Length + 1) / 4;
            List<char>[] oldlistOfCrates = new List<char>[numStack];
            for (int i = 0; i<numStack; i++)
                oldlistOfCrates[i] = new List<char>();
            while (!int.TryParse(data[index][1].ToString(), out s))
            {
                for(int i =0; i<numStack; i++)
                    if(data[index][4*i+1] != ' ')
                        oldlistOfCrates[i].Add(data[index][4*i+1]);
                index++;
            }
            
            List<char>[] listOfCrates = new List<char>[numStack];
            for (int i = 0; i < numStack; i++)
            {
                listOfCrates[i] = new List<char>();
                for (int j = oldlistOfCrates[i].Count - 1; j >= 0; j--)
                    listOfCrates[i].Add(oldlistOfCrates[i][j]);
            }
            
            index += 2;

            while (data[index] != "")
            {
                string[] stringSp = data[index].Split(' ');
                int count = Convert.ToInt32(stringSp[1]);
                int from = Convert.ToInt32(stringSp[3])-1;
                int to = Convert.ToInt32(stringSp[5])-1;
                
                while (count > 0)
                {
                    listOfCrates[to].Add(listOfCrates[from][listOfCrates[from].Count-1]);
                    listOfCrates[from].RemoveAt(listOfCrates[from].Count-1);
                    count--;
                }
                
                index++;
            }

            string sol = "";
            for (int i = 0; i < numStack; i++)
            {
                sol += listOfCrates[i][listOfCrates[i].Count - 1].ToString();
            }
            Console.WriteLine("Part1: " + sol);
        }

        private static void Part2(string[] data)
        {
            int s;
            int index = 0;
            int numStack = (data[0].Length + 1) / 4;
            List<char>[] oldlistOfCrates = new List<char>[numStack];
            for (int i = 0; i<numStack; i++)
                oldlistOfCrates[i] = new List<char>();
            while (!int.TryParse(data[index][1].ToString(), out s))
            {
                for(int i =0; i<numStack; i++)
                    if(data[index][4*i+1] != ' ')
                        oldlistOfCrates[i].Add(data[index][4*i+1]);
                index++;
            }
            
            List<char>[] listOfCrates = new List<char>[numStack];
            for (int i = 0; i < numStack; i++)
            {
                listOfCrates[i] = new List<char>();
                for (int j = oldlistOfCrates[i].Count - 1; j >= 0; j--)
                    listOfCrates[i].Add(oldlistOfCrates[i][j]);
            }
            
            index += 2;

            while (data[index] != "")
            {
                string[] stringSp = data[index].Split(' ');
                int count = Convert.ToInt32(stringSp[1]);
                int from = Convert.ToInt32(stringSp[3])-1;
                int to = Convert.ToInt32(stringSp[5])-1;
                
                while (count > 0)
                {
                    listOfCrates[to].Add(listOfCrates[from][listOfCrates[from].Count-count]);
                    listOfCrates[from].RemoveAt(listOfCrates[from].Count-count);
                    count--;
                }
                
                index++;
            }

            string sol = "";
            for (int i = 0; i < numStack; i++)
            {
                sol += listOfCrates[i][listOfCrates[i].Count - 1].ToString();
            }
            Console.WriteLine("Part1: " + sol);
        }
    }
}