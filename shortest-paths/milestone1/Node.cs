using System.Collections.Generic;
using System.Windows;

namespace NetworkClasses
{
    public class Node
    {
        public Node(Network network, Point center, string text)
        {
            Index = -1;
            Network = network;
            Center = center;
            Text = text;
            Links = new List<Link>();

            Network.AddNode(this);
        }

        public int Index { get; set; }
        
        public Network Network { get; }
        
        public Point Center { get; }
        
        public string Text { get; }
        
        public List<Link> Links { get; }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        public override string ToString()
        {
            return $"[{Text}]";
        }
    }
}
