using System;
using System.Collections.Generic;

namespace Algo
{
    public class StacksTest
    {
        private Stacks stack = new Stacks();

        public void IsValidTest()
        {
            var s = "{()}";
            System.Console.WriteLine(stack.IsValid(s));
        }

        public void BackspaceCompareTest()
        {
            var s = "#abc#";
            var t = "ab##";
            System.Console.WriteLine(stack.BackspaceCompare(s,t));
        }

        public void CalculateTest()
        {
            var s = "100-200-(100+200-(100-200-300))";
            System.Console.WriteLine(stack.CalculateExpression(s));
        }
    }

    public class Stacks
    {
        /// <summary>
        /// if parentheses matches
        /// </summary>
        public bool IsValid(string s)
        {
            if (s == null)
            {
                return false;
            }
            if (s == string.Empty)
            {
                return true;
            }

            string[] stack = new string[s.Length];
            int p = -1; 
            for (int i = 0; i < s.Length; i ++)
            {
                if (p == -1)
                {
                    stack[0] = s[i].ToString();
                    p ++;
                }
                else
                {
                    if (stack[p] == "{" && s[i].ToString() == "}" || stack[p] == "[" && s[i].ToString() == "]" || stack[p] == "(" && s[i].ToString() == ")")
                    {
                        p --;
                    }
                    else
                    {
                        stack[++p] = s[i].ToString();
                    }
                }
            }

            if (p == -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// LeetCode 844
        /// </summary>
        public bool BackspaceCompare(string S, string T)
        {
            if (S == T)
            {
                return true;
            }

            var stackS = new Stack<string>();
            var stackT = new Stack<string>();

            foreach (char c in S)
            {
                var str = c.ToString();
                if (str == "#")
                {
                    stackS.TryPop(out string s);
                }
                else
                {
                    stackS.Push(str);
                }
            }

            foreach (char c in T)
            {
                var str = c.ToString();
                if (str == "#")
                {
                    stackT.TryPop(out string s);
                }
                else
                {
                    stackT.Push(str);
                }
            }

            bool sNotEmpty;
            bool tNotEmpty;
            while (true)
            {
                sNotEmpty = stackS.TryPop(out string s);
                tNotEmpty = stackT.TryPop(out string t);
                if (!sNotEmpty && !tNotEmpty)
                {
                    return true;
                }
                if (s == t)
                {
                    continue;
                }
                return false;
            }
        }

        public int CalculateExpression(string s)
        {
            s = $"({s})";

            //revert string
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            s = new String(charArray);

            return Calculate(s);
        }

        private int Calculate(string s)
        {
            var nStack = new Stack<int>();
            var oStack = new Stack<string>();
            var startCalculate = false;
            var n = 0;

            foreach (char c in s)
            {
                string str = c.ToString();
                if (int.TryParse(str, out int number))
                {
                    if (n > 0)
                    {
                        nStack.Push(number * (int)Math.Pow(10,n) + nStack.Pop());
                    }
                    else
                    {
                        nStack.Push(number);
                    }
                    n++;
                }
                else
                {
                    if (str == " ")
                    {
                        continue;
                    }
                    if (str == "(") //end sub expression
                    {
                        startCalculate = true; // start to evaluate sub expression
                    }
                    if (startCalculate && oStack.TryPop(out string op))
                    {
                        if (op == ")")
                        {
                            startCalculate = false;
                            continue;
                        }

                        while (op != ")")
                        {
                            var n1 = nStack.Pop();
                            var n2 = nStack.Pop();

                            if (op == "+")
                            {
                                nStack.Push(n1 + n2);
                            }
                            else
                            {
                                nStack.Push(n1 - n2);
                            }

                            op = oStack.Pop();
                        }
                    }
                    else
                    {
                        oStack.Push(str);
                    }
                    startCalculate = false;
                    n = 0;
                }
            }

            if (oStack.TryPop(out string finalOp))
            {
                if (finalOp == "+")
                {
                    nStack.Push(nStack.Pop() + nStack.Pop());
                }
                else
                {
                    nStack.Push(nStack.Pop() - nStack.Pop());
                }
            }

            return nStack.Pop();
        }
    }

    public class MinStack
    {
        private Stack<int> stack;
        private int min = int.MaxValue;

        /** initialize your data structure here. */
        public MinStack() 
        {
            stack = new Stack<int>();
        }

        public void Push(int x)
        {
            if (x <= min) //x == min also need to update min value
            {
                //save last min value
                stack.Push(min);
                //update new min value
                min = x;
            }
            stack.Push(x);
        }

        public void Pop()
        {
            var x = stack.Pop();
            if (x == min)
            {
                //pop last min value
                var lastMin = stack.Pop();
                //update new min value
                min = lastMin;
            }
        }

        public int Top()
        {
            return stack.Peek();
        }

        public int GetMin()
        {
            return min;
        }
    }
}