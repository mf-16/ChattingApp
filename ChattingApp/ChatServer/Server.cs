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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatServer
{
    public class Server
    {

        public static int Port { get; set; } = 5555;
        private static Dictionary<string, Socket> _socketMap = new Dictionary<string, Socket>();
        private static object _lockObject = new object();   


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
                Task.Run(() => HandleClient(connectionSocket));
                

            }
        }

        public static void HandleClient(Socket socket){
            byte[] buffer = new byte[256];
            socket.Receive(buffer);
            var firstMessage = Encoding.UTF8.GetString(buffer).Split(";");
            var id = firstMessage[0];
            var name = firstMessage[1];
            Console.WriteLine(id);
            Console.WriteLine(name);

            lock (_lockObject)
            {
                _socketMap.Add(id, socket);
            }
            Console.WriteLine(name + " has connected!");
            BroadcastMessage(name + " has connected!");
            Array.Clear(buffer);
            
            while (true)
            {
                try
                {
                    socket.Receive(buffer);
                    var message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message);
                    BroadcastMessage(message);
                    Array.Clear(buffer);
                    
                } catch (SocketException)
                {
                    Console.WriteLine(name + " has disconnected!");
                    lock (_lockObject)
                    {
                        _socketMap.Remove(id);
                        BroadcastMessage(name + " has disconnected!");
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        return;
                    }
                  
                }
            }

        }

        public static void BroadcastMessage(string message)
        {
            
            foreach (var socket in _socketMap.Values)
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
            }

        }

   
    }
}
