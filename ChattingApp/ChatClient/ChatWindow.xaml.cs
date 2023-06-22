using ChatClient.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatClient
{
    public partial class ChatWindow : Window
    {
        private IO _connection;

        public ChatWindow(IO connection)
        {
            _connection = connection;
            InitializeComponent();

            Task.Run(() =>
            {
                while (true)
                {
                    var message = _connection.ReadMessage();
                    if (message.Length > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            listBox.Items.Add(message);
                        });
                    }
                }
            });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void MessageText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                string message = messageText.Text;
                _connection.SendMessage(message);
                messageText.Text = string.Empty;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = messageText.Text;
            _connection.SendMessage(message);
            messageText.Text = string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _connection.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            _connection.Socket.Close();
            Close();
        }
    }
}
