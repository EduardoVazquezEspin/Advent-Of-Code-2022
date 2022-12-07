using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AdventOfCode
{
    internal class Day2
    {
        public static void Solution(string[] args)
        {
            string[] lines = File.ReadAllLines("/home/eduard/Escriptori/Deures/AdventOfCode/AdventOfCode2/dades.txt");
            int value = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                char yourMove = Decrypt(lines[i][0], lines[i][2]);
                value += ValuePair(lines[i][0], yourMove);
                value += ValueInput(yourMove);
            }
            Console.WriteLine(value);
        }

        public static char Decrypt(char a, char result)
        {
            switch (result)
            {
                case 'X': // lose
                    switch (a)
                    {
                        case 'A':
                            return 'Z';
                        case 'B':
                            return 'X';
                        case 'C':
                            return 'Y';
                    }

                    break;
                case 'Y': // draw
                    switch (a)
                    {
                        case 'A':
                            return 'X';
                        case 'B':
                            return 'Y';
                        case 'C':
                            return 'Z';
                    }

                    break;
                
                case 'Z': // draw
                    switch (a)
                    {
                        case 'A':
                            return 'Y';
                        case 'B':
                            return 'Z';
                        case 'C':
                            return 'X';
                    }

                    break;
            }

            return 'c';
        }
        
        public static int ValueInput(char c)
        {
            switch (c)
            {
                case 'X':
                    return 1;
                case 'Y':
                    return 2;
                case 'Z':
                    return 3;
            }
            return 0;
        }

        public static int ValuePair(char a, char b)
        {
            switch (a.ToString() + b.ToString())
            {
                case "AY": case "BZ": case "CX":
                    return 6;
                case "AX": case "BY": case "CZ":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}