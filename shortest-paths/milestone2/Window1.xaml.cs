using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace draw_network
{
    public partial class Window1
    {
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

        private void DrawNetwork()
        {
            // Remove any previous drawing.
            mainCanvas.Children.Clear();

            // Make the network draw itself.
            MyNetwork.Draw(mainCanvas);

            MyNetwork.Links.ForEach(link => link.Draw(mainCanvas));
            MyNetwork.Links.ForEach(link => link.DrawLabel(mainCanvas));
            MyNetwork.Nodes.ForEach(node => node.Draw(mainCanvas));
        }

        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
