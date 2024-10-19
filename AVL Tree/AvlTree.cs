namespace AVL_Tree
{
    public class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; } = null;
        public Node? Right { get; set; } = null;
        public int Height { get; set; }
        public int BF { get; set; } // Balance Factor

        public Node(int value)
        {
            Value = value;
            Height = 0; // New nodes are initially added at height 1
        }
    }

    public class AvlTree
    {
        private Node? Root;

        public Node FindNode(int value)
        {
            return FindNodeRecursively(Root, value);
        }

        private Node FindNodeRecursively(Node node, int value)
        {
            if (node == null || value == node.Value)
                return node;
            if (node.Value < value)
                return FindNodeRecursively(node.Right, value);

            return FindNodeRecursively(node.Left, value);
        }

        public bool Find(int value)
        {
            return FindRecursively(Root, value);
        }

        private bool FindRecursively(Node? node, int value)
        {
            if (node == null)
                return false;

            if (node.Value == value)
                return true; // Value found

            return value < node.Value
                ? FindRecursively(node.Left, value)
                : FindRecursively(node.Right, value);
        }

        public void Insert(int value)
        {
            if (Root == null)
                Root = new Node(value);
            else
            {
                if (!Find(value))
                    Root = InsertRecursively(Root, value);
            }
        }

        private Node InsertRecursively(Node node, int value)
        {
            if (node == null)
                return new Node(value); // Create a new node if the current node is null

            if (value < node.Value)
            {
                node.Left = InsertRecursively(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = InsertRecursively(node.Right, value);
            }

            UpdateHeight(node); // Update the height of the current node
            return ReBalance(node); // Rebalance and return the new root of the subtree
        }
        public void Remove(int value)
        {
            Root = RemoveRecursively(Root, value);
        }
        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
        private Node FindMax(Node node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        private Node RemoveRecursively(Node node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
            {
                node.Left = RemoveRecursively(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = RemoveRecursively(node.Right, value);
            }
            else
            {
                // Node with the value found
                if (node.Left == null && node.Right == null)
                {
                    // No children
                    return null;
                }
                else if (node.Left == null)
                {
                    // One child (right)
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    // One child (left)
                    return node.Left;
                }
                else
                {
                    if (node.Left.Height > node.Right.Height)
                    {
                        var succssor = FindMax(node.Left);
                        node.Value = succssor.Value;
                        node.Left = RemoveRecursively(node.Left, succssor.Value);
                    }
                    else
                    {
                        var succssor = FindMin(node.Right);
                        node.Value = succssor.Value;
                        node.Right = RemoveRecursively(node.Right, succssor.Value);
                    }
                }
            }

            UpdateHeight(node); // Update the height of the current node
            return ReBalance(node); // Rebalance and return the new root of the subtree
        }


        private Node ReBalance(Node node)
        {
            if (node.BF > 1)
            {
                if (node.Left != null && node.Left.BF >= 0)
                    return RightRotate(node);
                if (node.Left != null)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }
            }
            else if (node.BF < -1)
            {
                if (node.Right != null && node.Right.BF <= 0)
                    return LeftRotate(node);
                if (node.Right != null)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }
            }

            return node;
        }

        private Node RightRotate(Node node)
        {
            Node newRoot = node.Left!;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            UpdateHeight(node); // Update height for the original root
            UpdateHeight(newRoot); // Update height for the new root
            return newRoot;
        }

        private Node LeftRotate(Node node)
        {
            Node newRoot = node.Right!;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            UpdateHeight(node); // Update height for the original root
            UpdateHeight(newRoot); // Update height for the new root
            return newRoot;
        }

        private void UpdateHeight(Node node)
        {
            int leftHeight = node.Left?.Height ?? -1;
            int rightHeight = node.Right?.Height ?? -1;
            node.Height = Math.Max(leftHeight, rightHeight) + 1;
            node.BF = leftHeight - rightHeight;
        }
        public Node FindPredecessor(int value)
        {
            var currentNode = FindNode(value);
            if (currentNode == null)
                return currentNode;
            if (currentNode.Left != null) ;
            return FindMax(currentNode.Left);

            Node predecessor = null;
            var ancestor = Root;
            while (ancestor != null)
            {
                if (value > ancestor.Value)
                {
                    predecessor = ancestor;
                    ancestor = predecessor.Right;
                }
                else
                {
                    ancestor = predecessor.Left;
                }
            }
        }
        public Node FindSuccssor(int value)
        {
            var currentNode = FindNode(value);
            if (currentNode == null)
                return currentNode;
            if (currentNode.Right != null) ;
            return FindMin(currentNode.Right);

            Node succssor = null;
            var ancestor = Root;
            while (ancestor != null)
            {
                if (value < ancestor.Value)
                {
                    succssor = ancestor;
                    ancestor = succssor.Left;
                }
                else
                {
                    ancestor = succssor.Right;
                }
            }
        }
        public void InOrderTraversal()
        {

            InOrderTraversal(Root);
        }
        private void InOrderTraversal(Node node)
        {

            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.WriteLine(node.Value);
                InOrderTraversal(node.Right);
            }
        }
        public void PreOrderTraversal()
        {

            PreOrderTraversal(Root);
        }
        private void PreOrderTraversal(Node node)
        {

            if (node != null)
            {
                Console.WriteLine(node.Value);
                PreOrderTraversal(node.Left);

                PreOrderTraversal(node.Right);
            }
        }
        public void PostOrderTraversal()
        {

            PostOrderTraversal(Root);
        }
        private void PostOrderTraversal(Node node)
        {

            if (node != null)
            {

                PostOrderTraversal(node.Left);

                PostOrderTraversal(node.Right);
                Console.WriteLine(node.Value);
            }
        }
        public void LevelOrder()
        {
            if (Root == null) return;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var visitedNode = queue.Dequeue();
                Console.WriteLine(visitedNode.Value);

                if (visitedNode.Left != null)
                    queue.Enqueue(visitedNode.Left);
                if (visitedNode.Right != null)
                    queue.Enqueue(visitedNode.Right);
            }
        }
    }
}
