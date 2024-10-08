﻿using ChatClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModel
{
    public class ServerConnector
    {
        public static int Port { get; set; } = 5555;
        public ServerConnector() {
        }
        public static Socket Connect()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostIP = IPAddress.Parse("192.168.1.26");
            IPEndPoint ep = new IPEndPoint(hostIP, Port);
            socket.Connect(ep);
            return socket;

        }
    }
}
