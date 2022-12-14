using System;
using System.IO;
using System.Xml.XPath;

namespace AdventOfCode
{
    public class Day14
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(14);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data) // 500,0
        {
            var limits = GetLimits(data);
            char[,] cave = DrawCave(limits, data);
            //PrintCave(cave);
            bool hasReachedDown = false;
            int unitsOfSand = 0;
            while (!hasReachedDown)
            {
                unitsOfSand++;
                int[] sandPos = {500 - limits.MinX, 0};
                while (SandCanFall(sandPos, cave) && !hasReachedDown)
                    hasReachedDown = sandPos[1] >= cave.GetLength(1);

                if (!hasReachedDown)
                    cave[sandPos[0], sandPos[1]] = 'o';
            }
            PrintCave(cave);
            PrintToFileCave("Day14 Cave1", cave);
            Console.WriteLine(--unitsOfSand);
        }
        
        public static void Part2(string[] data)
        {
            var limits = GetLimits(data);
            int height;
            char[,] cave = DrawCavePlus(limits, data, out height);
            //PrintCave(cave);
            bool hasReachedDown = false;
            int unitsOfSand = 0;
            while (!hasReachedDown)
            {
                unitsOfSand++;
                int[] sandPos = {height + 1, 0};
                while (SandCanFall(sandPos, cave)) { }

                cave[sandPos[0], sandPos[1]] = 'o';
                if (sandPos[0] == height + 1 && sandPos[1] == 0)
                    hasReachedDown = true;
            }
            PrintCave(cave);
            PrintToFileCave("Day14 Cave2", cave);
            Console.WriteLine(unitsOfSand);
        }

        public static Limits GetLimits(string[] data)
        {
            var result = new Limits();
            foreach (var line in data)
            {
                var lineSplit = line.Split(new string[] {" -> "}, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var subline in lineSplit)
                {
                    var sublineSplit = subline.Split(',');
                    int x = Convert.ToInt32(sublineSplit[0]);
                    int y = Convert.ToInt32(sublineSplit[1]);
                    if (!result.anyVal)
                    {
                        result.MinX = x;
                        result.MaxX = x;
                        result.MinY = y;
                        result.MaxY = y;
                        result.anyVal = true;
                    }

                    if (x < result.MinX) result.MinX = x;
                    if (x > result.MaxX) result.MaxX = x;
                    if (y < result.MinY) result.MinY = y;
                    if (y > result.MaxY) result.MaxY = y;
                }
            }

            return result;
        }

        public static char[,] DrawCave(Limits limits, string[] data)
        {
            char[,] result = new char[limits.MaxX - limits.MinX + 1, limits.MaxY + 1];
            
            for (int i = 0; i < result.GetLength(0); i++)
            for (int j = 0; j < result.GetLength(1); j++)
                result[i, j] = '.';
            
            foreach (var line in data)
            {
                var lineSplit = line.Split(new string[] {" -> "}, System.StringSplitOptions.RemoveEmptyEntries);
                int previousX = 0;
                int previousY = 0;
                for(int i =0; i<lineSplit.Length; i++)
                {
                    var sublineSplit = lineSplit[i].Split(',');
                    int x = Convert.ToInt32(sublineSplit[0]) - limits.MinX ;
                    int y = Convert.ToInt32(sublineSplit[1]);
                    if (i != 0)
                    {
                        MarkAllCellsBetween(new int[] {previousX, previousY}, new[] {x, y}, result);
                    }
                    previousX = x;
                    previousY = y;
                }
            }
            return result;
        }
        public static char[,] DrawCavePlus(Limits limits, string[] data, out int height)
        {
            height = limits.MaxY + 3;
            int width = 2 * height + 3;

            char[,] result = new char[width, height];
            
            for (int i = 0; i < result.GetLength(0); i++)
            for (int j = 0; j < result.GetLength(1); j++)
                result[i, j] = '.';

            for (int i = 0; i < result.GetLength(0); i++)
                result[i, height - 1] = '#';
            
            foreach (var line in data)
            {
                var lineSplit = line.Split(new string[] {" -> "}, System.StringSplitOptions.RemoveEmptyEntries);
                int previousX = 0;
                int previousY = 0;
                for(int i =0; i<lineSplit.Length; i++)
                {
                    var sublineSplit = lineSplit[i].Split(',');
                    int x =  Convert.ToInt32(sublineSplit[0]) - 500 + height + 1 ;
                    int y = Convert.ToInt32(sublineSplit[1]);
                    if (i != 0)
                        MarkAllCellsBetween(new int[] {previousX, previousY}, new[] {x, y}, result);
                    
                    previousX = x;
                    previousY = y;
                }
            }
            return result;
        }

        public static void PrintCave(char[,] cave)
        {
            for (int j =0; j<cave.GetLength(1); j++)
            {
                for(int i = 0; i < cave.GetLength(0); i++)
                    Console.Write(cave[i,j]);
                Console.Write("\n");
            }
        }

        public static void PrintToFileCave(string filename, char[,] cave)
        {
            string result = "";
            for (int j =0; j<cave.GetLength(1); j++)
            {
                for(int i = 0; i < cave.GetLength(0); i++)
                    result +=cave[i,j];
                result += "\n";
            }
            File.WriteAllText(filename + ".txt", result);
        }

        public static void MarkAllCellsBetween(int[] originPoint, int[] destinationPoint, char[,] map)
        {
            //Console.WriteLine(originPoint[0] + " " + originPoint[1] + " | " + destinationPoint[0] + " " + destinationPoint[1]);
            int[] movement = new[] {0, 0};
            if (originPoint[0] > destinationPoint[0]) movement[0] = -1;
            else if (originPoint[0] < destinationPoint[0]) movement[0] = 1;
            if (originPoint[1] > destinationPoint[1]) movement[1] = -1;
            else if (originPoint[1] < destinationPoint[1]) movement[1] = 1;
            while (originPoint[0] != destinationPoint[0] || originPoint[1] != destinationPoint[1])
            {
                map[originPoint[0], originPoint[1]] = '#';
                originPoint[0] += movement[0];
                originPoint[1] += movement[1];
            }
            map[originPoint[0], originPoint[1]] = '#';
        }

        public static bool SandCanFall(int[] sandPos, char[,] cave)
        {
            if (++sandPos[1]< 0) return true;
            if (sandPos[1] >= cave.GetLength(1)) return true;
            if (cave[sandPos[0], sandPos[1]] == '.') return true;
            if (cave[--sandPos[0], sandPos[1]] == '.') return true;
            sandPos[0] += 2;
            if (cave[sandPos[0], sandPos[1]] == '.') return true;
            sandPos[0]--;
            sandPos[1]--;
            return false;
        }
    }

    public class Limits
    {
        public bool anyVal;
        public int MinX;
        public int MaxX;
        public int MinY;
        public int MaxY;

        public Limits()
        {
            anyVal = false;
            MinX = 0;
            MaxX = 0;
            MinY = 0;
            MaxY = 0;
        }
    }
}