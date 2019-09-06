using System;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
}

public class RecursionTest
{
    Recursion r = new Recursion();

    public void TestLongestUnivaluePath()
    {
        var root = new TreeNode(1);
        root.left = new TreeNode(1);
        root.right = new TreeNode(1);
        root.left.left = new TreeNode(1);
        root.left.right = new TreeNode(1);
        // root.right.left = new TreeNode(5);
        // root.right.right = new TreeNode(4);
        // root.left.left.left = new TreeNode(5);
        // root.left.left.right = new TreeNode(4);
        // root.left.right.left = new TreeNode(5);

        Console.Write(r.LongestUnivaluePath(root));
    }
}

public class Recursion
{
    public int max = 0;

    ///<summary>
    /// LeetCode 687
    ///</summary>
    public int LongestUnivaluePath(TreeNode root)
    {
        LeetCode687(root);
        return max;
    }

    private int LeetCode687(TreeNode root)
    {
        if (root == null) return 0;

        var left = LeetCode687(root.left);
        var right = LeetCode687(root.right);
        int leftLength = 0, rightLength = 0;
        if (root.left != null && root.val == root.left.val)
        {
            leftLength += left + 1;
        }
        if (root.right != null && root.val == root.right.val)
        {
            rightLength += right + 1;
        }

        max = Math.Max(max, leftLength + rightLength);
        return Math.Max(leftLength, rightLength);
    }
}