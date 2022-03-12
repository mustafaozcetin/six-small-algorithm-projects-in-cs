using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace test_network
{
    public partial class Window1
    {
        private readonly Random random = new Random();

        public Window1()
        {
            InitializeComponent();
        }

        private Network MyNetwork = new Network();

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".net";
                dialog.Filter = "Network Files|*.net|All Files|*.*";

                // Display the dialog.
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    // Open the network.
                    MyNetwork = new Network(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyNetwork = new Network();
            }

            // Display the network.
            DrawNetwork();
        }

        private void MakeTestNetworks_Click(object sender, RoutedEventArgs e)
        {
            BuildGridNetwork("C:/6x10_grid.net", 600, 400, 6, 10);
            BuildGridNetwork("C:/10x15_grid.net", 600, 400, 10, 15);

            MessageBox.Show("Done");
        }

        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DrawNetwork()
        {
            // Remove any previous drawing.
            mainCanvas.Children.Clear();

            // Make the network draw itself.
            MyNetwork.Draw(mainCanvas);

            bool drawLabels = MyNetwork.Nodes.Count < 100;

            MyNetwork.Links.ForEach(link => link.Draw(mainCanvas));
            if (drawLabels)
            {
                MyNetwork.Links.ForEach(link => link.DrawLabel(mainCanvas));
            }
            MyNetwork.Nodes.ForEach(node => node.Draw(mainCanvas, drawLabels));
        }

        private Network BuildGridNetwork(string filename, double width, double height, int numRows, int numCols)
        {
            int colWidth = (int)Math.Round(width / numCols);
            int rowHeight = (int)Math.Round(height / numRows);

            var network = new Network();
            var nodes = new Node[numRows, numCols];

            // Create nodes
            const int Margin = 50;
            int centerX = Margin;
            int centerY = Margin;
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numCols; column++)
                {
                    Point nodeCenter = new Point(centerX, centerY);
                    int nodeText = numCols * row + column + 1;
                    nodes[row, column] = new Node(network, nodeCenter, nodeText.ToString());

                    centerX += colWidth;
                }
                centerX = Margin;
                centerY += rowHeight;
            }
            // Make horizontal links
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numCols - 1; column++)
                {
                    MakeRandomizedLink(network, nodes[row, column], nodes[row, column + 1]);
                }
            }
            // Make vertical links
            for (int row = 0; row < numRows - 1; row++)
            {
                for (int column = 0; column < numCols; column++)
                {
                    MakeRandomizedLink(network, nodes[row, column], nodes[row + 1, column]);
                }
            }

            network.SaveIntoFile(filename);

            return network;
        }

        private void MakeRandomizedLink(Network network, Node node1, Node node2)
        {
            double distance = GetDistance(node1.Center, node2.Center);

            double cost12 = GetCost(distance);
            new Link(network, node1, node2, cost12);

            double cost21 = GetCost(distance);
            new Link(network, node2, node1, cost21);
        }

        private double GetCost(double distance)
        {
            return Math.Round(distance * NextDouble(1.0, 1.2));
        }

        private double GetDistance(Point p1, Point p2)
        {
            return (p2 - p1).Length;
        }

        private double NextDouble(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
