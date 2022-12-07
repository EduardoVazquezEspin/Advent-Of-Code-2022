using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class SaveValue
    {
        public readonly ObjectArray Input;

        public SaveValue(object[] input)
        {
            Input = new ObjectArray(input);
        }

        public SaveValue(ObjectArray objectArray)
        {
            Input = objectArray;
        }
    }

    public class IntOperation
    {
        public readonly string _operation;
        public readonly int NumberOfParameters;

        public IntOperation(string operation)
        {
            _operation = operation;
            int total = 0;
            for(int i =0; i<operation.Length; i++)
                if (operation[i] == 'p')
                    total++;
            NumberOfParameters = total;
        }
        
        public IntOperation(string operation, int numberOfParameters)
        {
            _operation = operation;
            NumberOfParameters = numberOfParameters;
        }

        public int Solve(object[] parameters)
        {
            if (_operation == "")
                return 0;
            string operation = _operation;
            return Solve(ref operation, ref parameters);
        }

        private int Solve(ref string operation, ref object[] parameters)
        {
            char c = operation[0];
            operation = operation.Substring(1);
            if (c == 'p')
            {
                int res = (int) parameters[0];
                object[] newparams = new object[parameters.Length - 1];
                for (int i = 0; i < newparams.Length; i++)
                    newparams[i] = parameters[i + 1];
                parameters = newparams;
                return res;
            }
            if (c == '+')
            {
                int a = Solve(ref operation, ref parameters);
                int b = Solve(ref operation, ref parameters);
                return a + b;
            }
            if (c == '*')
            {
                int a = Solve(ref operation, ref parameters);
                int b = Solve(ref operation, ref parameters);
                return a * b;
            }
            if (c == '-')
            {
                int a = Solve(ref operation, ref parameters);
                int b = Solve(ref operation, ref parameters);
                return a - b;
            }
            if (c == '/')
            {
                int a = Solve(ref operation, ref parameters);
                int b = Solve(ref operation, ref parameters);
                return a / b;
            }
            if (c == 'n')
            {
                int a = Solve(ref operation, ref parameters);
                return -a;
            }
            int index = 0;
            int result = Convert.ToInt32(c.ToString());
            int x;
            while (int.TryParse(operation[index].ToString(), out x))
            {
                result = 10 * result + x;
                index++;
            }
            operation = operation.Substring(index);
            return result;
        }
    }

    public class ObjectArray
    {
        public object[] values;

        public int Length => values.Length;

        public ObjectArray(object[] array)
        {
            values = array;
        }
        
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var value in values)
                hash = 2 * hash + value.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ObjectArray)) return false;
            ObjectArray objarr = (ObjectArray) obj;
            if (Length != objarr.Length) return false;
            for(int i =0; i< Length; i++)
                if (!values[i].Equals(objarr.values[i]))
                    return false;
            return true;
        }
    }
    public class Wrapper
    {
        public Dictionary<ObjectArray, object> _cache;

        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<ObjectArray, object> pair in _cache)
                s+=pair.Key.values[0] + " | " + pair.Value + "\n";
            return s;
        }

        public static KeyValuePair<Func<object[], object[]>, int> FunctionCall(Func<object[], object[]> fun, int n)
        {
            return new KeyValuePair<Func<object[], object[]>, int>(fun, n);
        }
        
        public static object[] IntSum(object[] nums)
        {
            int result = 0;
            foreach (var num in nums)
                result += (int) num;
            return new object[] {result};
        }

        public static object[] IntDif(object[] nums)
        {
            return new object[] {(int)nums[0] -(int) nums[1]};
        }
        
        public static object[] IntProd(object[] nums)
        {
            int result = 1;
            foreach (var num in nums)
                result *= (int) num;
            return new object[] {result};
        }
        
        public static object[] IntDiv(object[] nums)
        {
            return new object[] {(int)nums[0] / (int) nums[1]};
        }

        public object Cache(Func<object[], object[]> function, int numberOfInputs, object[] inputs)
        {
            if (_cache == null)
                _cache = new Dictionary<ObjectArray, object>();
            
            Stack<object> callStack = new Stack<object>();
            Stack<object> memoryStack = new Stack<object>();
            
            callStack.Push(new KeyValuePair<Func<object[], object[]>, int>(function, numberOfInputs));
            for (int i = 0; i < numberOfInputs; i++)
                callStack.Push(inputs[i]);
                
            object top;
            do
            {
                top = callStack.Peek();
                callStack.Pop();
                Type topType = top.GetType();
                if (top is SaveValue)
                {
                    SaveValue saveValue = (SaveValue) top;
                    _cache.Add(saveValue.Input, memoryStack.Peek());
                }
                else if (top is IntOperation)
                {
                    IntOperation oper = (IntOperation) top;
                    object[] parameters = new object[oper.NumberOfParameters];
                    for (int i = 0; i < oper.NumberOfParameters; i++)
                    {
                        parameters[i] = memoryStack.Peek();
                        memoryStack.Pop();
                    }
                    int result = oper.Solve(parameters);
                    callStack.Push(result);
                }
                else if (topType.IsGenericType && topType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var pair = (KeyValuePair<Func<object[], object[]>, int>) top;
                    Func<object[], object[]> key = pair.Key;
                    int value = pair.Value;
                    object[] parameters = new object[value];
                    for (int i = 0; i < value; i++)
                    {
                        parameters[i] = memoryStack.Peek();
                        memoryStack.Pop();
                    }

                    object[] result;
                    if (_cache.ContainsKey(new ObjectArray(parameters)))
                    {
                        result = new[] {_cache[new ObjectArray(parameters)]};
                    }
                    else
                    {
                        result = key.Invoke(parameters);
                        if(key != IntSum && key != IntDif && key != IntProd && key != IntDiv) // OJOOO
                            callStack.Push(new SaveValue(parameters));
                    }
                    foreach (var it in result)
                        callStack.Push(it);
                }
                else
                    memoryStack.Push(top);
                
            } while (callStack.Any());

            return memoryStack.Peek();
        }
        
        public object IterativeRecursivity(Func<object[], object[]> function, int numberOfInputs, object[] inputs)
        {
            Stack<object> callStack = new Stack<object>();
            Stack<object> memoryStack = new Stack<object>();
            
            callStack.Push(new KeyValuePair<Func<object[], object[]>, int>(function, numberOfInputs));
            for (int i = 0; i < numberOfInputs; i++)
                callStack.Push(inputs[i]);
                
            object top;
            do
            {
                top = callStack.Peek();
                callStack.Pop();
                Type topType = top.GetType();
                if (topType.IsGenericType && topType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var pair = (KeyValuePair<Func<object[], object[]>, int>) top;
                    Func<object[], object[]> key = pair.Key;
                    int value = pair.Value;
                    object[] parameters = new object[value];
                    for (int i = 0; i < value; i++)
                    {
                        parameters[i] = memoryStack.Peek();
                        memoryStack.Pop();
                    }
                    object[] result = key.Invoke(parameters);
                    for(int i =0; i<result.Length; i++)
                        callStack.Push(result[i]);
                }
                else
                    memoryStack.Push(top);
                
            } while (callStack.Any());

            return memoryStack.Peek();
        }

        public string PrintCache()
        {
            string s = "";
            foreach (KeyValuePair<ObjectArray, object> pair in _cache)
            {
                s += "F(";
                for (int i = 0; i < pair.Key.values.Length; i++)
                {
                    if (i != 0)
                        s += ", ";
                    s += pair.Key.values[i];
                }
                s+=") = " + pair.Value + "\n";
            }
            return s;
        }
    }
}