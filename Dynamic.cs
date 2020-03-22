using System;

namespace Algo
{
    public class DynamicTest
    {
        private Dynamic dp = new Dynamic();

        public void TestKnapsack()
        {
            var itemsWeight = new int[] {2,2,4,6,3};
            var itemsValue = new int[] {3,4,8,9,6};
            var itemsNumber = itemsWeight.Length;
            var maxWeight = 9;

            Console.WriteLine("最大价值：" + dp.Knapsack(itemsWeight, itemsValue, itemsNumber, maxWeight));
        }
    }

    public class Dynamic
    {
        /// <summary>
        /// 0-1背包
        /// </summary>
        /// <param name="weight">物品重量数组</param>
        /// <param name="value">物品价值数组</param>
        /// <param name="n">物品个数</param>
        /// <param name="w">背包最大承重</param>
        public int Knapsack(int[] weight, int[] value, int n, int w)
        {
            int[][] states = new int[n][]; // 决策状态数组
            // 初始化 states
            for (int i = 0; i < n; ++i)
            {
                states[i] = new int[w+1];
                for (int j = 0; j < w+1; ++j)
                {
                    states[i][j] = -1;
                }
            }

            states[0][0] = 0;   // 不选第0个物品
            if (weight[0] <= w) // 第0个物品的重量 <= 背包重量
            {
                states[0][weight[0]] = value[0]; // 选择第0个物品
            }

            // 动态规划，状态转移
            for (int i = 1; i < n; ++i) // 循环遍历每个物品
            {
                // 不选择第 i 个物品
                for (int j = 0; j <= w; ++j)
                {
                    if (states[i-1][j] >= 0) 
                    {
                        states[i][j] = states[i-1][j]; // 价值状态跟上个物品一样
                    }
                }
                // 选择第 i 个物品
                for (int j = 0; j <= w-weight[i]; ++j)
                {
                    if (states[i-1][j] >= 0)
                    {
                        int v = states[i-1][j] + value[i];
                        if (v > states[i][j+weight[i]])
                        {
                            states[i][j+weight[i]] = v;
                        }
                    }
                }
            }
            // 找出最大值
            int maxvalue = -1;
            int maxvalueIndex = 0;
            for (int j = 0; j <= w; ++j)
            {
                if (states[n-1][j] > maxvalue)
                {
                    maxvalue = states[n-1][j];
                    maxvalueIndex = j;
                }
            }
            // 找出选择了哪些物品
            for (int i = n - 1; i >= 0; i--)
            {
                if (i == 0 && states[i][maxvalueIndex] > 0)
                {
                    Console.Write("第0 ");
                }
                if (maxvalueIndex-weight[i] >= 0 && states[i-1][maxvalueIndex-weight[i]] > -1 && states[i][maxvalueIndex] > states[i-1][maxvalueIndex]) // 选第i个物品
                {
                    Console.Write($"第{i} ");
                    maxvalueIndex = maxvalueIndex-weight[i];
                }
            }

            return maxvalue;
        }

        /// <summary>
        /// Leetcode 300 最长上升子序列长度
        /// </summary>
        public int LengthOfLIS(int[] nums)
        {
            var numsLength = nums.Length;
            if (numsLength == 0 || numsLength == 1)
            {
                return numsLength;
            }
            var state = new int[numsLength];
            var maxLength = 1;

            for (int i = 0; i < numsLength; i ++)
            {
                var currentLength = 1;
                for (int j = i - 1; j >= 0; j --)
                {
                    if (nums[j] < nums[i] && state[j] + 1 > currentLength)
                    {
                        currentLength = state[j] + 1;
                    }
                }
                state[i] = currentLength;
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                }
            }

            return maxLength;
        }
    }
}