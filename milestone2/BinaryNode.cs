using System;

namespace binary_node2
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
