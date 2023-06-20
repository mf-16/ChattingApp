using ChatClient.Model;
using ChatClient.ViewModel;
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
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private IO _connection;
        public ChatWindow(IO connection)
        {
            _connection = connection;
            InitializeComponent();
            Task.Run(() => {
                while (true)
                {
                    var message = _connection.ReadMessage();
                    Dispatcher.Invoke(() =>
                    {
                        listBox.Items.Add(message);
                    });
                }
            });


        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var message = messageText.Text;
            _connection.SendMessage(message);
        }
        
    }
}
