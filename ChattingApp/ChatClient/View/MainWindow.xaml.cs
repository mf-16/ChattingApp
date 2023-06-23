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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = text.Text;
            var user = new User(username);
            var connection = new IO(user);
            var chatWindow = new ChatWindow(connection);
            chatWindow.Show();
            Close();
        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                var username = text.Text;
                var user = new User(username);
                var connection = new IO(user);
                var chatWindow = new ChatWindow(connection);
                chatWindow.Show();
                Close();
            }
        }
    }
}
