using System;
using System.IO;

namespace AdventOfCode
{
    public static class InData
    {
        public static string[] ReadTxtFile(int fileName)
        {
            return File.ReadAllLines("/home/eduard/Escriptori/Deures/AdventOfCode/Dades/"+fileName+".txt");
        }
    }
}