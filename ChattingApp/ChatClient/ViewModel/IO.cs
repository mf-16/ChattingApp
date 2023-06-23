using ChatClient.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace ChatClient.ViewModel
{
    public class IO
    {
        public User User { get; set; }
        public Socket Socket { get; set; }
        public IO(User user)
        {
            User = user;
            Socket = ServerConnector.Connect();
            ConnectMessage();

        }
        private void ConnectMessage()
        {
            var msg = Encoding.UTF8.GetBytes(User.Id.ToString() + ";" + User.Username);
            Socket.Send(msg);
        }
        public void SendMessage(string message)
        {
            var msg = Encoding.UTF8.GetBytes("[" + DateTime.Now + "] " + User.Username + ": " + message);
            Socket.Send(msg);
        }
        public string ReadMessage()
        {
            byte[] buffer = new byte[256];
            Socket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer);
        }

    }
}
