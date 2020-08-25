### 树Tree
- **遍历Traversal**
  - 先序遍历Preorder Traversal（根节点在前面：root - left - right）
    - 自上而下依次访问子树的根节点然后才是左，右节点 
  - 中序遍历Inorder Traversal（根节点在中间：left - root - right）
  - 后序遍历Postorder Traversal（根节点在最后：left - right - root）
    - 自下而上依次访问子树的左，右节点最后才是根节点 
  - 递归遍历代码框架：
    ```c#
    public void Traversal(TreeNode root)
    {
        if (root == null) return;
        
        /*pre order*/
        Traversal(root.left);
        /*in order*/
        Traversal(root.right);
        /*post order*/
    }
    ```
  - 递归函数总结：
    - 递归是一种自下而上的思想。根据二叉树递归模板选择遍历模式
    - 若递归函数要参与计算则返回计算类型，若不参与计算则返回void
    - 想好递归出口
    - 想好递归公式或可重复子问题
  - 层级遍历Level Order Traversal(使用广度优先搜索思路，返回数组列表[[root],[left, right],[...]])
    - 解法1：循环。
    ```c#
    var resultList = new List<IList<int>>(); //创建数组列表作为最终结果
    var q = new Queue<int>(); //创建辅助队列
    var spNode = new TreeNode(int.MinValue); //创建分界节点（用于区分每一层）
    q.Enqueue(root.val); q.Enqueue(spNode); //将root节点加入队列，再将分界节点加入队列(第一层只有root)
    var levelList = new List<int>(); //创建每一层需要用到的数组
    while(q.TryDequeue(out TreeNode node)) //将队列出队循环，直至队列为空
    {
        if (node != spNode) //遇到正常节点
        {
            levelList.Add(node.val); //将当前节点加入层级数组
            if (node.left != null) q.Enqueue(node.left);
            if (node.right != null) q.Enqueue(node.right);
        }
        else //遇到分界节点，说明当前层已经遍历完毕
        {
            resultList.Add(levelList); //将当前层数组加入结果列表
            levelList = new List<int>(); //清空层级数组以便用于下一层
            if (q.Count > 0) q.Enqueue(spNode); //若队列不为空则在队尾插入分界节点
        }
    }
    ```
    - 解法2：递归。使用带层级信息的深度遍历（递归）
  - 全路径打印（**先序遍历**时将根节点依次加入string，返回string数组["1->2->3","1->4"]）
    ```c#
    var resultList = new List<string>(); //最终结果
    public void TraversePath(TreeNode node, string path) //递归函数，path用来记录路径，初始值为""
    {
        //将当前节点加入路径string
        if (path == "") path = node.val.ToString();
        else path += $"->{node.val.Tostring()}";
        if (node.left == null && node.right == null) //递归终止条件：遍历到叶子节点
        {
            resultList.Add(path); //将当前路径加入结果列表
            return;
        }
        if (node.left != null) TraversePath(node.left, path);
        if (node.right != null) TraversePath(node.right, path);
    }
    ```
  - 树的高度（**后序遍历**自下而上）
    ```c#
    public int maxDepth(TreeNode node)
    {
        if (node == null) return 0; //递归终止条件
        var leftDepth = maxDepth(node.left) + 1; //左子树每递进一次高度+1
        var rightDepth = maxDepth(node.right) + 1; //右子树每递进一次高度+1
        return Math.Max(leftDepth, rightDepth);
    }
    ```
  - 二叉树相同节点组成的同一条路径的最大路径值（**后序遍历**依次记录每个根节点的最大路径，层层往上）
    ```c#
    public int maxLength = 0;
    public int LongestUnivaluePath(TreeNode root)
    {
        Traverse(root);
        return maxLength;
    }
    
    public int Traverse(TreeNode node)
    {
        if (node == null) return 0; //递归出口
        var left = Traverse(node.left);
        var right = Traverse(node.right);
        
        var leftLength = 0; //记录左子树最大值
        var rightLength = 0; //记录右子树最大值
        if (node.left != null && node.val == node.left.val) //当前根节点与左节点相同
        {
            leftLength = left + 1;
        }
        if (node.right != null && node.val == node.right.val) //当前根节点与右节点相同
        {
            rightLength = right + 1;
        }
        if (node.left != null && node.right != null && node.val == node.left.val && node.val == node.right.val) //当前根节点与左右节点都相同
        {
            maxLength = Math.Max(maxLength, leftLength + rightLength); //记录仅经过当前根节点的最大路径
        }
        maxLength = Math.Max(maxLength, Math.Max(leftLength, rightLength)); //记录不经过当前根节点的情况（左右取最大值）
        return Math.Max(leftLength, rightLength); //返回左右取最大值作为上一层根节点的最大值
    }
    ```
- **搜索二叉树（Binary Search Tree, BST, B树）**
  - 定义：对于任何一个根节点，左子数所有节点小于根节点，右子树所有节点大于根节点
  - 特性：中序遍历返回一个有序数组
  - 搜索代码框架
    ```c#
    public bool isInBST(TreeNode root, int target)
    {
        if (root == null) return false;
        if (root.val == target) return true;
        if (target > root.val) return isInBST(root.right, target);
        if (target < root.val) return isInBST(root.left, target);
    }
    ```
  - 验证二叉树是否是BST代码框架
    ```c#
    public bool isBST(TreeNode root)
    {
        return isValidBST(root, null, null); //起始最小/最大值用null代替
    }
    
    //1.对于左子树，根节点就是最大值，所有左子树要小于根节点
    //2.对于右子树，根节点就是最小值，所有右子树要大于根节点
    public bool isValidBST(TreeNode node, int? minValue, int? maxValue)
    {
        if (node == null) return true; //空树也是BST
        if (minValue != null && node.val <= minValue) return false; //如果最小值不为空，当前节点却<=最小值
        if (maxValue != null && node.val >= maxValue) return false; //如果最大值不为空，当前节点却>=最大值
        return isValidBST(node.left, minValue, node.val) &&  //左子树，根节点就是最大值
               isValidBST(node.right, node.val, maxValue);   //右子树，根节点就是最小值
    }
    ```
  - 获取二叉树最大BST子树的size（Maximum BST subtree size）**==高频==**
    ```c#
    //结果类
    public class Result
    {
        public TreeNode node; //当前节点
        public int size;      //当前BST size
        public int min;       //左子树最小值
        public int max;       //右子树最大值
    }

    public int MaxBSTSize(TreeNode root)
    {
        Result r = Traverse(root);
        return r?.size ?? 0;
    }
    public Result Traverse(TreeNode node) //后序遍历，自下而上
    {
        if (node == null) return null;
        Result lTree = null; Result rTree = null;
        if (node.left != null) lTree = Traverse(node.left);
        if (node.right != null) rTree = Traverse(node.right);
        
        //左子树为空或当前节点大于左子树最大值
        bool isLTreeValid = (lTree == null || node.left == lTree.node && node.val > lTree.max);
        //右子树为空或当前节点小于右子树最小值
        bool isRTreeValid = (rTree == null || node.right == rTree.node && node.val < rTree.min);
        if (isLTreeValid && isRTreeValid) //左右子树都是BST
        {
            return new Result
            {
                node = node,
                size = (lTree?.size ?? 0) + (rTree?.size ?? 0) + 1, //size等于左右子树size相加再加当前节点
                min = lTree?.min ?? node.val, //左子树最小值作为当前BST最小值
                max = rTree?.max ?? node.val, //右子树最大值作为当前BST最大值
            };
        }
        //左右子树不全是BST则返回size较大一方
        return (lTree?.size ?? 0) > (rTree?.size ?? 0) ? lTree : rTree;
    }
    ```
  - 将BST就地转换成双向有序链表（画图助于理解） **==高频==** 
    ```c#
    public TreeNode head = null;
    //自下而上（后序遍历），对于每一个根节点：
    //1.将根节点与左子树最右一个叶子节点互连
    //2.将根节点与右子树最左一个叶子节点互联
    //最后返回中序遍历第一个叶子节点（左）或它的父节点（中）作为双向链表的头
    public TreeNode BST2LinkedList(TreeNode root)
    {
        if (root == null) return null;
        if (root.left != null) BST2LinkedList(root.left);
        if (head == null) head = root;
        if (root.right != null) BST2LinkedList(root.right);

        if (root.left != null)
        {
            //while(node.right != null) {node = node.right} return node;
            var rightMostNode = FindRightMostNode(root.left);
            root.left = rightMostNode;
            rightMostNode.right = root;
        }
        if (root.right != null)
        {
            //while(node.left != null) {node = node.left} return node;
            var leftMostNode = FindLeftMostNode(root.right);
            root.right = leftMostNode;
            leftMostNode.left = root;
        }

        return head;
    }
    ```

    ```
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
    ```
