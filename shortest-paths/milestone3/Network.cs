using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace test_network
{
    public class Network
    {
        public Network()
        {
            Clear();
        }

        public Network(string filePath)
        {
            ReadFromFile(filePath);
        }

        public List<Node> Nodes { get; private set; }

        public List<Link> Links { get; private set; }

        public void AddNode(Node node)
        {
            node.Index = Nodes.Count;
            Nodes.Add(node);
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        private void Clear()
        {
            Nodes = new List<Node>();
            Links = new List<Link>();
        }

        public string Serialization()
        {
            /*
             Sample string representation of the network:

             3 # Num nodes.
             3 # Num links.

             # Nodes.
             20,20,A
             120,20,B
             70,120,C
            
             # Links.
             0,1,100
             1,2,130
             0,2,50

             */

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Nodes.Count} # Num nodes.");
            stringBuilder.AppendLine($"{Links.Count} # Num links.");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("# Nodes.");
            foreach (var node in Nodes)
            {
                stringBuilder.AppendLine($"{node.Center.X},{node.Center.Y},{node.Text}");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("# Links.");
            foreach (var link in Links)
            {
                stringBuilder.AppendLine($"{link.FromNode.Index},{link.ToNode.Index},{link.Cost}");
            }

            return stringBuilder.ToString();
        }

        public void SaveIntoFile(string filePath)
        {
            File.WriteAllText(filePath, Serialization());
        }

        public void ReadFromFile(string filePath)
        {
            Deserialize(File.ReadAllText(filePath));
        }

        public Network LoadFromFile(string filePath)
        {
            string networkFileContent = File.ReadAllText(filePath);
            Deserialize(networkFileContent);
            return this;
        }

        private void Deserialize(string serialization)
        {
            Clear();

            // Get a stream to read the serialization one line at a time.
            using (StringReader reader = new StringReader(serialization))
            {
                // Get the number of nodes and links.
                int num_nodes = int.Parse(ReadNextLine(reader));
                int num_links = int.Parse(ReadNextLine(reader));

                // Read the nodes.
                for (int i = 0; i < num_nodes; i++)
                {
                    // Read the next node's values.
                    string[] fields = ReadNextLine(reader).Split(',');
                    double x = double.Parse(fields[0]);
                    double y = double.Parse(fields[1]);
                    string text = fields[2].Trim();

                    // Make the node. (This adds the node to the network.)
                    new Node(this, new Point(x, y), text);
                }

                // Read the links.
                for (int i = 0; i < num_links; i++)
                {
                    // Read the next link's values.
                    string[] fields = ReadNextLine(reader).Split(',');
                    int index1 = int.Parse(fields[0]);
                    int index2 = int.Parse(fields[1]);
                    double cost = double.Parse(fields[2]);

                    // Make the link. (This adds the link to the network.)
                    new Link(this, Nodes[index1], Nodes[index2], cost);
                }
            }
        }

        private string ReadNextLine(StringReader reader)
        {
            // Repeat until we get a line or reach the end.
            for (; ; )
            {
                // Get the next line.
                string line = reader.ReadLine();

                // If we've reached the end of the stream, return null.
                if (line == null)
                    return null;

                // Trim comments.
                line = line.Split('#')[0];
                line = line.Trim();

                // If the line is non-blank, return it.
                if (line.Length > 0)
                    return line;
            }
        }

        public void Draw(Canvas mainCanvas)
        {
            Rect networkBounds = GetBounds();
            Size margin = new Size(50, 50);
            networkBounds.Inflate(margin);
            mainCanvas.SetElementBounds(networkBounds);
        }

        public Rect GetBounds()
        {
            IEnumerable<Point> centers = Nodes.Select(node => node.Center);
            
            double minX = centers.Min(center => center.X);
            double maxX = centers.Max(center => center.X);
            
            double minY = centers.Min(center => center.Y);
            double maxY = centers.Max(center => center.Y);

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }
    }
}