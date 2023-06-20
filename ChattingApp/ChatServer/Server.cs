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

        public static void Start()
        {
           
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] addresses = Dns.GetHostEntry(IPAddress.Loopback).AddressList;
            IPAddress hostIP = addresses[0];
            IPEndPoint ep = new IPEndPoint(hostIP, Port);
            socket.Bind(ep);
            socket.Listen(100);
            while (true)
            {
                byte[] buffer = new byte[256];
                var connectionSocket = socket.Accept();
                int bytes = connectionSocket.Receive(buffer);
                if (bytes > 256)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                }
                

            }

            

        }

  



    }
}
