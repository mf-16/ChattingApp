using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {

        public static int Port { get; set; } = 5555;

        private static List<Socket> _sockets = new List<Socket>();


        public static void Start()
        {
           
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostIP = IPAddress.Parse("192.168.1.26");
            IPEndPoint ep = new IPEndPoint(hostIP, Port);
            socket.Bind(ep);
            socket.Listen(100);
          
            while (true)
            {
                var connectionSocket = socket.Accept();
                _sockets.Add(connectionSocket);
                Task.Run(() => HandleClient(connectionSocket));
                

            }
        }

        public static void HandleClient(Socket socket){
            byte[] buffer = new byte[256];
            while (true)
            {
                try
                {
                    socket.Receive(buffer);
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                    BroadcastMessage(buffer);
                    Array.Clear(buffer);
                    
                } catch (SocketException)
                {

                    _sockets.Remove(socket);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }

        }

        public static void BroadcastMessage(byte[] message)
        {
            foreach (var socket in _sockets)
            {
                socket.Send(message);
            }

        }

   
    }
}
