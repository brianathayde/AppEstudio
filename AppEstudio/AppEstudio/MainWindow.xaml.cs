using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AppEstudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {


        TcpClient client;
        NetworkStream stream;
        GerenciadorTwitter g;
        Thread t;
        

        void Atualizar()
        {
            while (true)
            {
                Connect(g.Tweet());
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            g = new GerenciadorTwitter();

            t = new Thread(Atualizar);
            t.Start();
            Thread.Sleep(20);
        }


        void Connect(string message)
        {
            string server = "127.0.0.1";
            
            if (client == null)
            {
                try
                {
                    Int32 port = 13000;
                    client = new TcpClient(server, port);
                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream = client.GetStream();
                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);

                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
                catch
                {
                    client = null;
                    try
                    {
                        Int32 port = 13000;
                        client = new TcpClient(server, port);
                        // Translate the passed message into ASCII and store it as a Byte array.
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                        stream = client.GetStream();
                        // Send the message to the connected TcpServer. 
                        stream.Write(data, 0, data.Length);

                    }
                    catch
                    {
                    }
                }

            }
        }
   

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            t.Abort();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GerenciadorTwitter.TAG = textBox.Text;
        }
    }

}
