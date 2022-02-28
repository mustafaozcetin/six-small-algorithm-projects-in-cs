using System;
using System.Collections.Generic;

namespace nary_node4
{
    class NaryNode<T>
    {
        public NaryNode(T value)
        {
            Value = value;
            Children = new List<NaryNode<T>>();
        }
        public T Value { get; set; }

        public List<NaryNode<T>> Children { get; set; }

        public void AddChild(NaryNode<T> node)
        {
            Children.Add(node);
        }

        public NaryNode<T> FindNode(T valueToFind)
        {
            if (Value.Equals(valueToFind))
            {
                return this;
            }
            foreach (var childNode in Children)
            {
                var node = childNode.FindNode(valueToFind);
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }

        public List<NaryNode<T>> TraversePreorder()
        {
            var result = new List<NaryNode<T>>();

            result.Add(this);

            foreach (var childNode in Children)
            {
                var children = childNode.TraversePreorder();
                result.AddRange(children);
            }

            return result;
        }

        public List<NaryNode<T>> TraversePostorder()
        {
            var result = new List<NaryNode<T>>();

            foreach (var childNode in Children)
            {
                var children = childNode.TraversePostorder();
                result.AddRange(children);
            }

            result.Add(this);

            return result;
        }

        public List<NaryNode<T>> TraverseBreadthFirst()
        {
            List<NaryNode<T>> result = new List<NaryNode<T>>();
            Queue<NaryNode<T>> queue = new Queue<NaryNode<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                NaryNode<T> node = queue.Dequeue();
                result.Add(node);

                foreach (var childNode in node.Children)
                {
                    queue.Enqueue(childNode);
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
            string stringValue = $"{spaces}{Value}:{newLine}";

            foreach (NaryNode<T> childNode in Children)
            {
                stringValue += childNode.ToString(spaces + "  ");
            }
            return stringValue;
        }
    }
}
