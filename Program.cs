using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Algo
{
    public class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(10);
            root.left = new TreeNode(5);
            root.right = new TreeNode(15);
            root.right.left = new TreeNode(11);
            root.right.right = new TreeNode(20);

            //var result = new Program().LetterCombinations("23");
            var result = new Program().GenerateParenthesis(3);
        }

#region BackTrack
        public List<string> parenthResult = new List<string>();
        public IList<string> GenerateParenthesis(int n)
        {
            ParenthBackTrack(new StringBuilder(), n, n);
            return parenthResult;
        }

        public void ParenthBackTrack(StringBuilder path, int leftRemain, int rightRemain)
        {
            if (leftRemain > rightRemain) return;
            else if (leftRemain < 0 || rightRemain < 0) return;
            else if (leftRemain == 0 && rightRemain == 0)
            {
                parenthResult.Add(path.ToString());
                return;
            }

            foreach (var s in new string[]{"(", ")"})
            {
                path.Append(s);
                if (s == "(")
                {
                    ParenthBackTrack(path, leftRemain - 1, rightRemain);
                }
                else if (s == ")")
                {
                    ParenthBackTrack(path, leftRemain, rightRemain - 1);
                }
                path = path.Remove(path.Length - 1, 1);
            }
        }

        public Dictionary<char, string> letterDic = new Dictionary<char, string>
        {
            ['1'] = "", ['2'] = "abc", ['3'] = "def", ['4'] = "ghi", ['5'] = "jkl", ['6'] = "mno",
            ['7'] = "pqrs", ['8'] = "tuv", ['9'] = "wxyz"
        };
        public List<string> result = new List<string>();
        public IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits)) return result;
            BackTrack("", digits);
            return result;
        }

        public void BackTrack(string path, string digits)
        {
            if (digits.Length == 0)
            {
                result.Add(path);
                return;
            }

            foreach (var c in letterDic[digits[0]])
            {
                path += c.ToString();
                BackTrack(path, digits.Remove(0, 1));
                path = path.Remove(path.Length - 1, 1);
            }
        }
#endregion

        // public TreeNode str2tree(String s) //"2(1()())(3()())"
        // {
        //     if (string.IsNullOrEmpty(s)) return null;
        //     TreeNode root = new TreeNode(int.Parse(s[0].ToString()));
        //     TreeNode left = null;
        //     TreeNode right = null;
        //     //for (int i = )

        // }

        public int pathSum = int.MinValue;
        public int MaxPathSum(TreeNode root)
        {
            RootSumTraversal(root);
            return pathSum;
        }
        public int RootSumTraversal(TreeNode node)
        {
            if (node == null) return 0;
            var leftSum = RootSumTraversal(node.left);
            var rightSum = RootSumTraversal(node.right);
            var rootSum = node.val;

            if (node.left != null)
            {
                if (leftSum > pathSum) pathSum = node.left.val;
                if (leftSum + node.val > pathSum) pathSum = leftSum + node.val;
                if (leftSum + node.val > rootSum) rootSum = leftSum + node.val;
            }
            if (node.right != null)
            {
                if (rightSum > pathSum) pathSum = node.right.val;
                if (rightSum + node.val > pathSum) pathSum = rightSum + node.val;
                if (rightSum + node.val > rootSum) rootSum = rightSum + node.val;
            }
            if (leftSum + rightSum + node.val > pathSum) pathSum = leftSum + rightSum + node.val;
            if (node.val > pathSum) pathSum = node.val;
            return rootSum;
        }

        // [[0,1],[0,2],[2,3],[2,4],[2,5]]

        // Get memoery address of an int variable
        static string GetIntAddress(int i)
        {
            unsafe
            {
                int* ptr = &i;
                IntPtr addr = (IntPtr)ptr;
                return addr.ToString("x");
            }
        }

        static IEnumerable<int> GenerateWithYield()
        { 
            var i = 0;
            while (i<5)
                yield return ++i; 
        }
    }
}
