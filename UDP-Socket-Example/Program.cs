using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Socket_Example
{
    class Program
    {
        private const int BufferSize = 1024;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter 'S' for server 'C' for client");

            string input = Console.ReadLine();

            if (input.Equals("S"))
            {
                Console.WriteLine("Starting server");
                Server();
            }
            else if (input.Equals("C"))
            {
                Console.WriteLine("Starting client");
                Client();
            }
            else
            {
                Console.WriteLine("Unexpected input!");
            }

            Console.ReadLine();
        }

        private static void Server()
        {
            // Create server socket
            Socket serverSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
            int serverPortNum = 50000;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIpAddress, serverPortNum);
            serverSocket.Bind(serverEndPoint);

            // Listen for incoming message
            byte[] receivedBytes = new byte[BufferSize];
            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            serverSocket.ReceiveFrom(receivedBytes, ref clientEndPoint);

            // Log received message to the user
            string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
            Console.WriteLine(receivedMessage);

            // Echo received message
            serverSocket.SendTo(receivedBytes, clientEndPoint);

            // Close socket
            serverSocket.Close();
        }

        private static void Client()
        {
            // Create client socket
            Socket clientSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            IPAddress clientIpAddress = IPAddress.Parse("127.0.0.1");
            int clientPortNum = 60000;
            IPEndPoint clientEndPoint = new IPEndPoint(clientIpAddress, clientPortNum);
            clientSocket.Bind(clientEndPoint);

            // Send a message to server
            IPAddress serverIPAddress = IPAddress.Parse("127.0.0.1");
            int serverPortNum = 50000;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, serverPortNum);
            string messageToSend = "Hello World";
            byte[] bytesToSend = Encoding.ASCII.GetBytes(messageToSend);
            clientSocket.SendTo(bytesToSend, serverEndPoint);

            // Listen for incoming message
            byte[] receivedBytes = new byte[BufferSize];
            clientSocket.Receive(receivedBytes);

            // Log received message to user
            string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
            Console.WriteLine(receivedMessage);
        }
    }
}
