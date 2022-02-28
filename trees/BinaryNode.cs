using System;
using System.Collections.Generic;

namespace binary_node4
{
    class BinaryNode<T>
    {
        public BinaryNode(T value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }
        public T Value { get; set; }

        public BinaryNode<T> LeftChild { get; set; }

        public BinaryNode<T> RightChild { get; set; }

        public void AddLeft(BinaryNode<T> node)
        {
            LeftChild = node;
        }

        public void AddRight(BinaryNode<T> node)
        {
            RightChild = node;
        }

        public BinaryNode<T> FindNode(T valueToFind)
        {
            if (Value.Equals(valueToFind))
            {
                return this;
            }
            if (LeftChild != null)
            {
                var node = LeftChild.FindNode(valueToFind);
                if (node != null)
                {
                    return node;
                }
            }
            if (RightChild != null)
            {
                var node = RightChild.FindNode(valueToFind);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        public List<BinaryNode<T>> TraversePreorder()
        {
            var result = new List<BinaryNode<T>>();
            
            result.Add(this);

            if (LeftChild != null)
            {
                var leftChildren = LeftChild.TraversePreorder();
                if (leftChildren != null)
                {
                    result.AddRange(leftChildren);
                }
            }

            if (RightChild != null)
            {
                var rightChildren = RightChild.TraversePreorder();
                if (rightChildren != null)
                {
                    result.AddRange(rightChildren);
                }
            }

            return result;
        }

        public List<BinaryNode<T>> TraverseInorder()
        {
            var result = new List<BinaryNode<T>>();

            if (LeftChild != null)
            {
                var leftChildren = LeftChild.TraverseInorder();
                if (leftChildren != null)
                {
                    result.AddRange(leftChildren);
                }
            }

            result.Add(this);

            if (RightChild != null)
            {
                var rightChildren = RightChild.TraverseInorder();
                if (rightChildren != null)
                {
                    result.AddRange(rightChildren);
                }
            }

            return result;
        }

        public List<BinaryNode<T>> TraversePostorder()
        {
            var result = new List<BinaryNode<T>>();

            if (LeftChild != null)
            {
                var leftChildren = LeftChild.TraversePostorder();
                if (leftChildren != null)
                {
                    result.AddRange(leftChildren);
                }
            }

            if (RightChild != null)
            {
                var rightChildren = RightChild.TraversePostorder();
                if (rightChildren != null)
                {
                    result.AddRange(rightChildren);
                }
            }
            
            result.Add(this);

            return result;
        }

        public List<BinaryNode<T>> TraverseBreadthFirst()
        {
            List<BinaryNode<T>> result = new List<BinaryNode<T>>();
            Queue<BinaryNode<T>> nodeQueue = new Queue<BinaryNode<T>>();

            // Start with the top node in the queue.
            nodeQueue.Enqueue(this);

            while (nodeQueue.Count > 0)
            {
                // Remove the top node from the queue and add it to the result list.
                BinaryNode<T> node = nodeQueue.Dequeue();
                result.Add(node);

                // Add the node's children to the queue.
                if (node.LeftChild != null)
                {
                    nodeQueue.Enqueue(node.LeftChild);
                }
                
                if (node.RightChild != null)
                {
                    nodeQueue.Enqueue(node.RightChild);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return ToString("  ");
        }

        private string ToString(string spaces)
        {
            string newLine = Environment.NewLine;
            string stringValue = $"{spaces}{Value}: {newLine}";
            const string nullNodeValue = "None";

            if (LeftChild == null && RightChild == null)
            {
                return stringValue;
            }

            if (LeftChild != null)
            {
                stringValue += LeftChild.ToString(spaces + "  ");
            }
            else
            {
                stringValue += $"{spaces}  {nullNodeValue}{newLine}";
            }

            if (RightChild != null)
            {
                stringValue += RightChild.ToString(spaces + "  ");
            }
            else
            {
                stringValue += $"{spaces}  {nullNodeValue}{newLine}";
            }

            return stringValue;
        }
    }
}
