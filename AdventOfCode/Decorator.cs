using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Decorator
    {
        public static Func<T1,T2> Timed<T1, T2>(Func<T1, T2> function)
        {
            return input =>
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                T2 result = function(input);
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                return result;
            };
        }
        
        public static Func<T> Timed<T>(Func<T> function)
        {
            return () =>
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                T result = function();
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                return result;
            };
        }
        
        public static Action Timed(Action function)
        {
            return () =>
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                function();
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            };
        }

        public static void WrapperExample()
        {
            Wrapper wrapper1 = new Wrapper();
            int value = 50;
            Wrapper wrapper2 = new Wrapper();
            Timed(() =>
            {
                object result=  wrapper2.Cache(RecursivityExample2, 1, new object[] {value});
                Console.WriteLine(result);
            })();
            Timed(() =>
            {
                object result=  wrapper1.Cache(RecursivityExample, 1, new object[] {value});
                Console.WriteLine(result);
            })();
            Console.WriteLine(wrapper1.PrintCache());
        }
        
        public static object[] Fibonacci(object[] n)
        {
            if ((int) n[0] <= 1)
                return new[] {n[0]};
            return new object[]
            {
                Wrapper.FunctionCall(Wrapper.IntSum, 2),
                Wrapper.FunctionCall(Fibonacci, 1),
                (int) n[0]-1,
                Wrapper.FunctionCall(Fibonacci, 1),
                (int) n[0]-2
            };
        }

        public static object[] RecursivityExample(object[] n) // a_n = 3*a_{n-1} - 2*a_{n-2}, a_0 = 7, a_1 = 5
        {
            int num = (int) n[0];
            if (num == 0) return new object[] {7};
            if (num == 1) return new object[] {5};
            return new object[]
            {
                Wrapper.FunctionCall(Wrapper.IntDif, 2),
                Wrapper.FunctionCall(Wrapper.IntProd, 2),
                3,
                Wrapper.FunctionCall(RecursivityExample, 1),
                num - 1,
                Wrapper.FunctionCall(Wrapper.IntProd, 2),
                2,
                Wrapper.FunctionCall(RecursivityExample, 1),
                num - 2
            };
        }

        public static object[] RecursivityExample2(object[] n)
        {
            int num = (int) n[0];
            var fun = Wrapper.FunctionCall(RecursivityExample2, 1);
            if (num == 0) return new object[] {7};
            if (num == 1) return new object[] {5};
            return new object[]
            {
                new IntOperation("-*3p*2p"),
                fun,
                num -1,
                fun,
                num -2
            };
        }
    }
}