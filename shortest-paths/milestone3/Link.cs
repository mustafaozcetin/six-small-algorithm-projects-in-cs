using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace test_network
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

        public void Draw(Canvas canvas)
        {
            canvas.DrawLine(FromNode.Center, ToNode.Center, Brushes.LimeGreen, 2);
        }

        public void DrawLabel(Canvas canvas)
        {
            double dx = ToNode.Center.X - FromNode.Center.X;
            double dy = ToNode.Center.Y - FromNode.Center.Y;

            double angleInRadians = Math.Atan2(dy, dx);
            double angleInDegrees = angleInRadians * (180 / Math.PI);

            double costLabelX = 0.67 * FromNode.Center.X + 0.33 * ToNode.Center.X;
            double costLabelY = 0.67 * FromNode.Center.Y + 0.33 * ToNode.Center.Y;

            Rect costEllipseBounds = new Rect(costLabelX - 10, costLabelY - 10, 20, 20);
            canvas.DrawEllipse(costEllipseBounds, Brushes.White, Brushes.White, 2);

            canvas.DrawString(Cost.ToString(), 20, 20, new Point(costLabelX, costLabelY), angleInDegrees, 12, Brushes.Black);
        }

        public override string ToString()
        {
            return $"{FromNode} --> {ToNode} ({Cost})";
        }
    }
}