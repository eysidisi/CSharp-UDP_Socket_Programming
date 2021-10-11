using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDP_Socket_Example.Core;

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

                UDPSocket server = new UDPSocket("127.0.0.1", 50000);
                string receivedMessage = server.Echo();

                Console.WriteLine($"Server received {receivedMessage}");
            }
            else if (input.Equals("C"))
            {
                Console.WriteLine("Starting client");

                UDPSocket client = new UDPSocket("127.0.0.1", 60000);
                client.Send("Hello World", "127.0.0.1", 50000);
                string receivedMessage = client.Listen();

                Console.WriteLine($"Client received {receivedMessage}");
            }
            else
            {
                Console.WriteLine("Unexpected input!");
            }
            Console.WriteLine("Press any key to quit!");
            Console.ReadLine();
        }
    }
}
