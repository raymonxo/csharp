using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _server;
        private int _port;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Capture server & port values
                _server = serverTextBox.Text;
                _port = Convert.ToInt32(portTextBox.Text);

                // Send all requests
                var numRequests = Convert.ToInt32(numRequestsTextBox.Text);
                AppendMessageSafe("Sending " + numRequests + " requests");
                for (int i = 0; i < numRequests; ++i)
                {
                    AppendMessageSafe("Spawning thread " + i);
                    int threadI = i;    // Must create copy of i so thread's don't share
                    var thread = new Thread(() => SendRequest(threadI));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                AppendMessageSafe(ex.ToString());
            }
        }

        private void SendRequest(int id)
        {
            try
            {
                // Establish connection to server
                using (var client = new TcpClient(_server, _port))
                using (var writer = new BinaryWriter(client.GetStream()))
                using (var reader = new BinaryReader(client.GetStream()))
                {
                    // Send request
                    var request = "hello" + id;
                    writer.Write(request);
                    AppendMessageSafe("Thread " + id + " sent: " + request);

                    // Read response
                    var response = reader.ReadString();
                    AppendMessageSafe("Thread " + id + " received: " + response);
                }
            }
            catch (Exception ex)
            {
                AppendMessageSafe(ex.ToString());
            }
        }

        // Safe way to add messages to text box from background thread
        private void AppendMessageSafe(string message)
        {
            messagesTextBox.Dispatcher.Invoke(
                new AppendMessageCallback(AppendMessage),
                new object[] { message });
        }
        private delegate void AppendMessageCallback(string message);
        private void AppendMessage(string message)
        {
            messagesTextBox.AppendText(message + '\n');
        }
    }
}
