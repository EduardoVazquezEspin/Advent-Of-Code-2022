using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day11
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(11);
            Part1(data);
            Part2(data);
        }

        public static void Part1(string[] data)
        {
            Console.WriteLine("PART 1");
            List<Monkey> monkeys = ReadInput(data);
            for(int i =0; i<20; i++)
                DoOneRound(ref monkeys, 3, 9699690);
            foreach (var monkey in monkeys)
            {
                Console.WriteLine(monkey.NumberOfInspections);
            }
        }
        
        public static void Part2(string[] data)
        {
            Console.WriteLine("PART 2");
            List<Monkey> monkeys = ReadInput(data);
            for (int i = 0; i < 10000; i++)
                DoOneRound(ref monkeys, 1, 9699690);
            foreach (var monkey in monkeys)
            {
                Console.WriteLine(monkey.NumberOfInspections);
            }
        }

        public static void DoOneRound(ref List<Monkey> list, int relief, int mcd)
        {
            foreach (var monkey in list)
            {
                while (monkey.Items.Any())
                {
                    monkey.Items[0] = monkey.Operation(monkey.Items[0]);
                    monkey.Items[0] /= relief;
                    monkey.Items[0] %= mcd;
                    int resMon = monkey.Result(monkey.Test(monkey.Items[0]));
                    list[resMon].Items.Add(monkey.Items[0]);
                    monkey.Items.RemoveAt(0);
                    monkey.NumberOfInspections++;
                }
            }
        }
        
        public static List<Monkey> ReadInput(string[] data)
        {
            List<Monkey> resultingList = new List<Monkey>();
            resultingList.Add(new Monkey(
                    new List<long>(),
                    it => it * 13,
                    it => it % 2 == 0,
                    val => val ? 5 : 2
                ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it + 7,
                it => it % 13 == 0,
                val => val ? 4 : 3
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it + 2,
                it => it % 5 == 0,
                val => val ? 5 : 1
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it * 2,
                it => it % 3 == 0,
                val => val ? 6 : 7
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it * it,
                it => it % 11 == 0,
                val => val ? 7 : 3
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it + 6,
                it => it % 17 == 0,
                val => val ? 4 : 1
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it + 1,
                it => it % 7 == 0,
                val => val ? 0 : 2
            ));
            resultingList.Add(new Monkey(
                new List<long>(),
                it => it + 8,
                it => it % 19 == 0,
                val => val ? 6 : 0
            ));
            for (int i = 0; i < data.Length / 7; i++)
            {
                string worries = data[7 * i + 1].Substring(18);
                worries = worries.Replace(" ", "");
                string[] worriesSplit = worries.Split(',');
                foreach(var element in worriesSplit)
                    resultingList[i].Items.Add(Convert.ToInt32(element));
            }
            return resultingList;
        }
        
        public static List<Monkey> Example()
        {
            List<Monkey> resultingList = new List<Monkey>();
            resultingList.Add(new Monkey(
                    new List<long>() {79, 98},
                    it => it * 19,
                    it => it % 23 == 0,
                    val => val ? 2 : 3
                ));
            resultingList.Add(new Monkey(
                new List<long>() {54, 65, 75, 74},
                it => it + 6,
                it => it % 19 == 0,
                val => val ? 2 : 0
            ));
            resultingList.Add(new Monkey(
                new List<long>() {79, 60, 97},
                it => it * it,
                it => it % 13 == 0,
                val => val ? 1 : 3
            ));
            resultingList.Add(new Monkey(
                new List<long>() {74},
                it => it + 3,
                it => it % 17 == 0,
                val => val ? 0 : 1
            ));
            return resultingList;
        }
    }

    public class Monkey
    {
        public List<long> Items;
        public Func<long, long> Operation;
        public Func<long, bool> Test;
        public Func<bool, int> Result;
        public int NumberOfInspections;

        public Monkey(List<long> items, Func<long, long> operation, Func<long, bool> test, Func<bool, int> result)
        {
            Items = items;
            Operation = operation;
            Test = test;
            Result = result;
            NumberOfInspections = 0;
        }
    }
}