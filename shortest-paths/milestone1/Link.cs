namespace NetworkClasses
{
    public class Link
    {
        public Link(Network network, Node fromNode, Node toNode, double cost)
        {
            Network = network;
            FromNode = fromNode;
            ToNode = toNode;
            Cost = cost;

            Network.AddLink(this);
            FromNode.AddLink(this);
        }

        public Network Network { get; }
        
        public Node FromNode { get; }
        
        public Node ToNode { get; }
        
        public double Cost { get; }

        public override string ToString()
        {
            return $"{FromNode} --> {ToNode} ({Cost})";
        }
    }
}
