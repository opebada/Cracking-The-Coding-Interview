using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter4_TreesAndGraphs
{
    public class TreesAndGraphs
    {
        public static int CountPathsWithSum(TreeNode<int> root, int targetSum)
        {
            if (root == null) return 0;

            return CountPathsWithSum(root, targetSum, 0, new Dictionary<int, int>());
        }

        public static int CountPathsWithSum(TreeNode<int> node, int targetSum,
            int runningSum, Dictionary<int, int> pathCount)
        {
            if (node == null) return 0;

            runningSum += node.Data;
            int sum = runningSum - targetSum;
            int totalPaths = (pathCount.ContainsKey(runningSum)) ? pathCount[sum] : 0;

            if (runningSum == targetSum)
                totalPaths += runningSum;

            IncrementHashTable(pathCount, runningSum, 1); // increment pathCount
            totalPaths += CountPathsWithSum(node.Left, targetSum, runningSum, pathCount);
            totalPaths += CountPathsWithSum(node.Right, targetSum, runningSum, pathCount);
            IncrementHashTable(pathCount, runningSum, -1); // decrement pathCount

            return totalPaths;
        }

        private static void IncrementHashTable(Dictionary<int, int> table, int key, int delta)
        {
            int newCount = table.ContainsKey(key) ? table[key] + delta : delta;

            if (newCount == 0)
                table.Remove(key);
            else
                table[key] = newCount;
        }
        public static int PathWithSum(TreeNode<int> root, int targetSum)
        {
            if (root == null) return 0;

            int pathsFromRoot = PathWithSumHelper(root, targetSum, 0);

            int pathsOnLeft = PathWithSum(root.Left, targetSum);
            int pathsOnRight = PathWithSum(root.Right, targetSum);

            return pathsFromRoot + pathsOnLeft + pathsOnRight;
        }

        public static int PathWithSumHelper(TreeNode<int> node, int targetSum, int currentSum)
        {
            if (node == null) return 0;

            currentSum += node.Data;

            int totalPaths = 0;
            if (currentSum == targetSum)
                return totalPaths++;

            totalPaths += PathWithSumHelper(node.Left, targetSum, currentSum);
            totalPaths += PathWithSumHelper(node.Right, targetSum, currentSum);
            return totalPaths;
        }

        /// <summary>
        /// Implementation of a binary tree class with a GetRandomNode method
        /// which returns a random node from the tree
        /// </summary>
        public class Tree 
        {
            public TreeNode Root { get; set; }
            public int Size { get; set; }

            public TreeNode GetRandomNode()
            {
                Random rand = new Random();
                int index = rand.Next(Size);
                return Root.GetIthNode(index);
            }

            public void InsertInOrder(int d)
            {
                Root.InsertInOrder(d);
            }

            public class TreeNode
            {
                public int Data { get; set; }
                public TreeNode Left { get; set; }
                public TreeNode Right { get; set; }
                public int Size { get; set; }

                public TreeNode(int d)
                {
                    Data = d;
                    Size = 1;
                }

                public TreeNode GetIthNode(int i)
                {
                    int leftSize = Left == null ? 0 : Left.Size;

                    if (i < leftSize)
                    {
                        return Left.GetIthNode(i);
                    } else if (i == leftSize)
                    {
                        return this;
                    } else
                    {
                        return Right.GetIthNode(i - (leftSize + 1));
                    }
                }

                public void InsertInOrder(int d)
                {
                    if (d <= Data)
                    {
                        if (Left == null)
                        {
                            Left = new TreeNode(d);
                        }
                        else
                        {
                            Left.InsertInOrder(d);
                        }
                    }
                    else
                    {
                        if (Right == null)
                        {
                            Right = new TreeNode(d);
                        }
                        else
                        {
                            Right.InsertInOrder(d);
                        }
                    }

                    Size++;
                }

                public TreeNode Find(int d)
                {
                    if (Data == d)
                    {
                        return this;
                    }
                    else if (d < Data)
                    {
                        return Left == null ? null : Left.Find(d);
                    }
                    else
                    {
                        return Right == null ? null : Right.Find(d);
                    }
                }
            }
        }
        public static bool CheckSubtree1(TreeNode<int> t1, TreeNode<int> t2)
        {
            if (t1 == null || t2 == null) return false;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            GetPreorderString(t1, sb1);
            GetPreorderString(t2, sb2);

            string string1 = sb1.ToString();
            string string2 = sb2.ToString();

            if (string1.Contains(string2))
                return true;

            return false;
        }

        private static void GetPreorderString(TreeNode<int> node, StringBuilder sb)
        {
            if (node == null)
            {
                sb.Append('n');
                return;
            } else
            {
                sb.Append(node.Data);
                GetPreorderString(node.Left, sb);
                GetPreorderString(node.Right, sb);
            }
        }

        public static bool CheckSubtree(TreeNode<int> t1, TreeNode<int> t2)
        {
            if (t1 == null) return false;

            if (t1.Data == t2.Data && MatchTree(t1, t2))
                return true;

            bool isOnleft = CheckSubtree(t1.Left, t2);
            bool isOnRight = CheckSubtree(t1.Right, t2);

            return isOnleft || isOnRight;
        }

        public static bool MatchTree(TreeNode<int> t1, TreeNode<int> t2)
        {
            if (t1 == null && t2 == null) return true;
            if (t1.Data != t2.Data) return false;
            if (t1 == null || t2 == null) return false;

            bool left = MatchTree(t1.Left, t2.Right);
            bool right = MatchTree(t1.Right, t2.Right);

            return left && right;
        }
        public static TreeNode<int> CommonAncestor(TreeNode<int> root, TreeNode<int> p, TreeNode<int> q)
        {
            if (!Covers(root, p) || !Covers(root, q))
            {
                return null;
            }

            return FirstCommonAncestorHelper(root, p, q);
        }

        public static TreeNode<int> FirstCommonAncestorHelper(TreeNode<int> root, TreeNode<int> p, TreeNode<int> q)
        {
            if (root == null) return null;
            if (root == p || root == q) return root;

            bool pIsOnLeft = Covers(root.Left, p);
            bool qIsOnLeft = Covers(root.Left, q);

            if (pIsOnLeft != qIsOnLeft) // Nodes are on different sides
            {
                return root;
            }

            TreeNode<int> childSide = pIsOnLeft ? root.Left : root.Right;
            return FirstCommonAncestorHelper(childSide, p, q);
        }


        /// <summary>
        /// Finds the first common ancestor of two nodes in a binary tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static TreeNode<int> FirstCommonAncestor(TreeNode<int> root, TreeNode<int> p, TreeNode<int> q)
        {
            // check to see if the nodes are in the tree
            if (!Covers(root, p) || !Covers(root, q))
            {
                return null;
            } else if (Covers(p, q))
            {
                return p;
            } else if (Covers(q, p))
            {
                return q;
            }

            if (p.Parent == null) return null;

            TreeNode<int> parent = p.Parent;
            TreeNode<int> sibling = GetSibling(p);

            while (!Covers(sibling, q))
            {
                sibling = GetSibling(parent);
                parent = parent.Parent;
            }

            return parent;
        }

        private static TreeNode<int> GetSibling(TreeNode<int> node)
        {
            if (node == null) return null;
            if (node.Parent == null) return null;

            TreeNode<int> parent = node.Parent;

            TreeNode<int> sibling = (node == parent.Left) ? parent.Right : parent.Left;

            return sibling;
        }

        private static bool Covers(TreeNode<int> root, TreeNode<int> q)
        {
            if (root == null) return false;
            if (root == q) return true;

            bool coveredByLeft = Covers(root.Left, q);
            bool coveredByRight = Covers(root.Right, q);

            return coveredByLeft || coveredByRight;
        }
        /// <summary>
        /// Finds the first common ancestor of two nodes in a binary tree
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static TreeNode<int> FirstCommonAncestor(TreeNode<int> p, TreeNode<int> q)
        {
            if (p == null || q == null) return null;

            int pDepth = GetDepth(p);
            int qDepth = GetDepth(q);

            TreeNode<int> shallow = (pDepth < qDepth) ? p : q;
            TreeNode<int> deeper = (pDepth < qDepth) ? q : p;

            deeper = GoUp(deeper, Math.Abs(pDepth - qDepth));

            while (shallow != deeper && shallow != null && deeper != null)
            {
                shallow = shallow.Parent;
                deeper = deeper.Parent;
            }

            if (shallow == null || deeper == null)
                return null;

            return shallow;
        }

        private static TreeNode<int> GoUp(TreeNode<int> node, int k)
        {
            if (node == null) return null;

            while (node != null && k > 0)
            {
                node = node.Parent;
                k--;
            }

            return node;
        }

        private static int GetDepth(TreeNode<int> node)
        {
            if (node == null) return 0;

            int depth = 0;

            while (node != null)
            {
                node = node.Parent;
                depth++;
            }

            return depth;
        }
        /// <summary>
        /// Finds a valid build order
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="edges"></param>
        public static void BuildOrder(List<GraphNode<string>> projects, List<Edge> edges)
        {
            if (projects == null || projects.Count == 0) return;

            foreach (Edge e in edges)
            {
                e.Out.Children.Add(e.In);
                e.In.Parents.Add(e.Out);
            }

            foreach (GraphNode<string> g in projects)
            {
                // find node whose parent is null, it must be the root
                if (g.Parents.Count == 0)
                {
                    printOrder(g);
                }
            }
        }

        static void printOrder(GraphNode<string> root)
        {
            Queue<GraphNode<string>> q = new Queue<GraphNode<string>>();
            q.Enqueue(root);

            while (q.Count > 0)
            {
                GraphNode<string> r = q.Dequeue();
                Console.WriteLine(r.Data);

                foreach (GraphNode<string> n in r.Children)
                {
                    if (n.Visited == false)
                    {
                        n.Visited = true;
                        q.Enqueue(n);
                    }
                }
            }
        }

        static void printOrderDFS(GraphNode<string> root)
        {
            if (root == null) return;

            Console.WriteLine(root);
            root.Visited = true;

            foreach (GraphNode<string> n in root.Children)
            {
                if (n.Visited == false)
                {
                    printOrderDFS(n);
                }
            }
        }

        /// <summary>
        /// Finds the in-order successor of a given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNode<int> Successor(TreeNode<int> node)
        {
            if (node == null) return null;

            // found right children -> return leftmost node of right subtree.
            if (node.Right != null)
            {
                return leftMostChild(node);
            } else
            {
                TreeNode<int> q = node;
                TreeNode<int> x = q.Parent;

                while (x != null && x.Left != q)
                {
                    q = x;
                    x = x.Parent;
                }

                return x;
            }
        }

        private static TreeNode<int> leftMostChild(TreeNode<int> n)
        {
            if (n == null) return null;

            while (n.Left != null)
            {
                n = n.Left;
            }

            return n;
        }

        /// <summary>
        /// Checks if a binary tree is a binary search trees
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsBST(TreeNode<int> node)
        {
            if (node == null) return true;

            bool left = IsBST(node.Left);
            bool right = IsBST(node.Right);

            if (left == false || right == false)
                return false;

            int leftValue = (node.Left != null) ? node.Left.Data : -1;
            int rightValue = (node.Right != null) ? node.Right.Data : -1;

            if (leftValue != -1 && leftValue > node.Data)
                return false;

            if (rightValue != -1 && rightValue < node.Data)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if a binary tree is balanced
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsBalanced(TreeNode<int> node)
        {
            if (node == null) return false;

            int left = GetTreeHeight(node.Left);
            int right = GetTreeHeight(node.Right);

            if (Math.Abs(left - right) <= 1)
                return true;

            return false;
        }

        private static int GetTreeHeight(TreeNode<int> root)
        {
            if (root == null) return -1;

            int left = GetTreeHeight(root.Left);
            int right = GetTreeHeight(root.Right);

            return Math.Max(left, right) + 1;
        }

        /// <summary>
        /// Creates a linked list of all the nodes at each depth of a binary tree
        /// </summary>
        /// <param name="root">The root of the tree</param>
        /// <returns></returns>
        public static List<LinkedList<TreeNode<int>>> ListOfDepth(TreeNode<int> root)
        {
            if (root == null) return null;

            List<LinkedList<TreeNode<int>>> list = new List<LinkedList<TreeNode<int>>>();
            LinkedList<TreeNode<int>> current = new LinkedList<TreeNode<int>>();
            current.AddLast(root);
            
            while (current.Count > 0)
            {
                list.Add(current);

                LinkedList<TreeNode<int>> parents = current;
                current = new LinkedList<TreeNode<int>>();

                foreach (TreeNode<int> parent in parents)
                {
                    if (parent.Left != null)
                        current.AddLast(parent.Left);

                    if (parent.Right != null)
                        current.AddLast(parent.Right);
                }
            }

            return list;
        }
         
        public static TreeNode<int> CreateMinimalBST(int[] array, int low, int high)
        {
            if (low > high) return null;

            int mid = (high + low) / 2;

            TreeNode<int> root = new TreeNode<int>();
            root.Data = array[mid];

            root.Left = CreateMinimalBST(array, low, mid - 1);
            root.Right = CreateMinimalBST(array, mid + 1, high);

            if (root.Left != null)
                root.Left.Parent = root;

            if (root.Right != null)
                root.Right.Parent = root;

            return root;
        }
        /// <summary>
        /// Finds if there is a route between two nodes
        /// </summary>
        /// <param name="s">Source node</param>
        /// <param name="t">Destination node</param>
        /// <returns></returns>
        public static bool PathExists(GraphNode<string> s, GraphNode<string> t)
        {
            if (s == null || t == null) return false;
            if (s.Data == t.Data) return true;

            Queue<GraphNode<string>> queue = new Queue<GraphNode<string>>();
            s.Marked = true;
            queue.Enqueue(s);

            while (queue.Count > 0)
            {
                GraphNode<string> r = queue.Dequeue();

                if (r != null)
                {
                    foreach (GraphNode<string> node in r.Children)
                    {
                        if (s.Data == node.Data)
                            return true;

                        if (node.Marked == false)
                        {
                            node.Marked = true;
                            queue.Enqueue(node);
                        }
                    }
                }
            }

            return false;
        }
    }
}
