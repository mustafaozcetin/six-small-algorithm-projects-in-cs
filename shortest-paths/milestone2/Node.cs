using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace draw_network
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

        public void Draw(Canvas canvas)
        {
            Rect nodeBounds = new Rect(Center.X - 10, Center.Y - 10, 20, 20);
            canvas.DrawEllipse(nodeBounds, Brushes.White, Brushes.Black, 1);

            canvas.DrawString(Text, 20, 20, Center, 0, 12, Brushes.Blue);
        }

        public override string ToString()
        {
            return $"[{Text}]";
        }
    }
}