using System;
using System.Collections.Generic;

namespace nary_node2
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
