
namespace Algo
{
    // 二叉树节点
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }

        public TreeNode(int[] values, int index = 0)
        {
            CreateBinaryTree(this, values, index);
        }

        private void CreateBinaryTree(TreeNode node, int[] values, int index)
        {
            node.val = values[index];
            if (index * 2 + 1 < values.Length)
            {
                node.left = new TreeNode(values, index * 2 + 1);
            }
            if (index * 2 + 2 < values.Length)
            {
                node.right = new TreeNode(values, index * 2 + 2);
            }
        }
    }
}