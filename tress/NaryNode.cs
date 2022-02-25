using System.Collections.Generic;
using System.Linq;

namespace nary_node1
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
            var childValues = Children.Select(c => c.Value);
            return string.Format("{0}: {1}", Value, string.Join(" ", childValues));
        }
    }
}
