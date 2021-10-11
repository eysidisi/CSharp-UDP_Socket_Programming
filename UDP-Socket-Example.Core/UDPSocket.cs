using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Socket_Example.Core
{
    public class UDPSocket
    {

        Socket _socket;
        const int BufferSize = 1024;

        public const int SIO_UDP_CONNRESET = -1744830452;

        public UDPSocket(string ipAddress, int portNum)
        {
            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _socket.IOControl(
            (IOControlCode)SIO_UDP_CONNRESET,
            new byte[] { 0, 0, 0, 0 },
            null);
            IPAddress parsedIpAddress = IPAddress.Parse(ipAddress);
            IPEndPoint localEndPoint = new IPEndPoint(parsedIpAddress, portNum);
            _socket.Bind(localEndPoint);
        }

        public string Listen()
        {
            byte[] receivedBytes = new byte[BufferSize];
            _socket.Receive(receivedBytes);

            // Log received message to user
            string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
            return receivedMessage;
        }

        public void Send(string messageToSend, string ipAddress, int portNum)
        {
            IPAddress serverIPAddress = IPAddress.Parse(ipAddress);
            IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, portNum);
            byte[] bytesToSend = Encoding.ASCII.GetBytes(messageToSend);
            _socket.SendTo(bytesToSend, serverEndPoint);
        }


        public string Echo()
        {
            byte[] receivedBytes = new byte[BufferSize];
            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _socket.ReceiveFrom(receivedBytes, ref clientEndPoint);

            // Log received message to the user
            string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

            // Echo received message
            _socket.SendTo(receivedBytes, clientEndPoint);

            return receivedMessage;
        }
    }
}
