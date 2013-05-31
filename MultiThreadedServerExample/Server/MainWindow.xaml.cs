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

namespace Server
{
    /// <summary>
    /// Main window allowing configuration, starting, and stopping of server.
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isRunning = false;
        private TcpListener _listener;
        private Thread _processingThread;
        private ConcurrentQueue<TcpClient> _clientQueue = new ConcurrentQueue<TcpClient>();
        private Random _random = new Random();
 
        public MainWindow()
        {
            InitializeComponent();
            ConfigureUI();
            _processingThread = new Thread(ProcessClients);
            _processingThread.IsBackground = true;
            _processingThread.Start();
        }

        private void startStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        private void Start()
        {
            try
            {
                // Start listening
                var server = IPAddress.Parse("127.0.0.1");
                var port = Convert.ToInt32(portTextBox.Text);
                _listener = new TcpListener(server, port);
                _listener.Start();
                _listener.BeginAcceptTcpClient(OnAccept, null);

                // Indicate running
                _isRunning = true;

                // Update UI
                ConfigureUI();
            }
            catch (Exception ex)
            {
                AppendMessageSafe(ex.ToString());

                if (_listener != null)
                {
                    _listener.Stop();
                }
            }
        }

        private void Stop()
        {
            try
            {
                // Stop listening
                if (_listener != null)
                {
                    _listener.Stop();
                }

                // Indicate not running
                _isRunning = false;

                // Update UI
                ConfigureUI();
            }
            catch (Exception ex)
            {
                AppendMessageSafe(ex.ToString());
            }
        }

        private void OnAccept(IAsyncResult result)
        {
            try
            {
                // Add client to queue
                var client = _listener.EndAcceptTcpClient(result);
                if (client != null)
                {
                    _clientQueue.Enqueue(client);
                    AppendMessageSafe("Accepted client connection: " + client.ToString());
                }

                // Resume listening if still running
                _listener.BeginAcceptTcpClient(OnAccept, null);
            }
            catch (ObjectDisposedException)
            {
                // The listener was Stop()'d which resulted in the underlying socket being
                // disposed followed by the completion of the async accept which triggered the
                // call to this callback.  Since this callback was called in response to the
                // termination of the async accept and not in response to a client connection
                // the attempt to begin another async accept in the try block above failed
                // resulting in this exception.  Since the server is being stopped and we don't
                // want to resume listening for client connections this exception can be safely
                // ignored.  See the following for more details:
                // http://stackoverflow.com/questions/1173774/stopping-a-tcplistener-after-calling-beginaccepttcpclient
            }
            catch (Exception ex)
            {
                AppendMessageSafe(ex.ToString());
            }
        }

        private void ProcessClients()
        {
            // Loop forever processing any clients that appear in queue
            // TODO: Change to shutdown processing while not running.  Be sure to ensure all
            //       pending clients are processed before shutdown can complete.
            while (true)
            {
                // Sleep until client appears
                // TODO: Replace this loop with observer pattern so notified immediatly when item
                //       appears in queue.
                while (_clientQueue.IsEmpty)
                {
                    Thread.Sleep(100);
                }

                // Process next client
                try
                {
                    TcpClient client;
                    if (_clientQueue.TryDequeue(out client))
                    {
                        using (client)
                        using (var reader = new BinaryReader(client.GetStream()))
                        using (var writer = new BinaryWriter(client.GetStream()))
                        {
                            // Read request
                            string request = reader.ReadString();
                            AppendMessageSafe("Received request: " + request);

                            // Simulate delay due to work being done by server
                            Thread.Sleep(_random.Next() % 2000);

                            // Send response
                            string response = "ACK " + request;
                            writer.Write(response);
                            AppendMessageSafe("Sent response: " + response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppendMessageSafe(ex.ToString());
                }
            }
        }

        private void ConfigureUI()
        {
            portTextBox.IsEnabled = !_isRunning;
            startStopButton.Content = _isRunning ? "Stop" : "Start";
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
