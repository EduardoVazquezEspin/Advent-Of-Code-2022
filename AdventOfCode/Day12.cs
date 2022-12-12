using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AdventOfCode
{
    public static class Day12
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(12);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            int[] initialPos = FindValue('S', data);
            int rows = data.Length;
            int cols = data[0].Length;
            bool[,] hasBeenReached = new bool[rows, cols];
            for(int i =0; i<rows; i++)
            for (int j = 0; j < cols; j++)
                hasBeenReached[i, j] = false;
            
            Queue<KeyValuePair<int[], int>> nextPos = new Queue<KeyValuePair<int[], int>>();
            nextPos.Enqueue(new KeyValuePair<int[], int>(initialPos, 0));
            hasBeenReached[initialPos[0], initialPos[1]] = true;
            bool hasFoundExit = false;
            int result = -1;
            while (!hasFoundExit && nextPos.Any())
            {
                var top = nextPos.Peek();
                nextPos.Dequeue();
                List<int[]> neighbours = GetNeighbours(top.Key, data);
                foreach (var neighbour in neighbours)
                {
                    if (!hasBeenReached[neighbour[0], neighbour[1]] && CanMoveFrom(top.Key, neighbour, data))
                    {
                        nextPos.Enqueue(new KeyValuePair<int[], int>(neighbour, top.Value+1));
                        hasBeenReached[neighbour[0], neighbour[1]] = true;
                        if (data[neighbour[0]][neighbour[1]] == 'E')
                        {
                            hasFoundExit = true;
                            result = top.Value + 1;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
        
        public static void Part2(string[] data)
        {
            List<int[]> potentialInitialPos = FindAll('a', data);
            potentialInitialPos.Add(FindValue('S', data));
            int rows = data.Length;
            int cols = data[0].Length;
            List<int> results = new List<int>();
            foreach (var initialPos in potentialInitialPos)
            {
                bool[,] hasBeenReached = new bool[rows, cols];
                for(int i =0; i<rows; i++)
                for (int j = 0; j < cols; j++)
                    hasBeenReached[i, j] = false;
            
                Queue<KeyValuePair<int[], int>> nextPos = new Queue<KeyValuePair<int[], int>>();
                nextPos.Enqueue(new KeyValuePair<int[], int>(initialPos, 0));
                hasBeenReached[initialPos[0], initialPos[1]] = true;
                bool hasFoundExit = false;
                int result = -1;
                while (!hasFoundExit && nextPos.Any())
                {
                    var top = nextPos.Peek();
                    nextPos.Dequeue();
                    List<int[]> neighbours = GetNeighbours(top.Key, data);
                    foreach (var neighbour in neighbours)
                    {
                        if (!hasBeenReached[neighbour[0], neighbour[1]] && CanMoveFrom(top.Key, neighbour, data))
                        {
                            nextPos.Enqueue(new KeyValuePair<int[], int>(neighbour, top.Value+1));
                            hasBeenReached[neighbour[0], neighbour[1]] = true;
                            if (data[neighbour[0]][neighbour[1]] == 'E')
                            {
                                hasFoundExit = true;
                                result = top.Value + 1;
                            }
                        }
                    }
                }

                results.Add(result);
            }

            Console.WriteLine(results.Where(it => it != -1).Min());
        }

        public static int[] FindValue(char c, string[] data)
        {
            for(int i =0; i< data.Length; i++) for(int j =0; j<data[i].Length; j++)
                if (data[i][j] == c)
                    return new[] {i, j};
            return new int[0];
        }

        public static List<int[]> GetNeighbours(int[] pos, string[] data)
        {
            List<int[]> result = new List<int[]>();
            if(pos[0] > 0) result.Add(new []{pos[0]-1, pos[1]});
            if(pos[1] > 0) result.Add(new []{pos[0], pos[1]-1});
            if(pos[0] < data.Length-1) result.Add(new []{pos[0]+1, pos[1]});
            if(pos[1] < data[0].Length-1) result.Add(new []{pos[0], pos[1]+1});
            return result;
        }

        public static bool CanMoveFrom(int[] from, int[] to, string[] data)
        {
            char fromC = data[from[0]][from[1]];
            if (fromC == 'S')
                fromC = 'a';
            char toC = data[to[0]][to[1]];
            if (toC == 'E')
                toC = 'z';
            return 1+ (int) fromC >= (int) toC;
        }

        public static List<int[]> FindAll(char c, string[] data)
        {
            List<int[]> results = new List<int[]>();
            for(int i =0; i< data.Length; i++) for(int j =0; j<data[i].Length; j++)
                if (data[i][j] == c)
                    results.Add(new[] {i, j});
            return results;
        }
    }
}