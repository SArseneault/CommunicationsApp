using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creating new server object
            Server server = new Server();

            Console.WriteLine("Establishing Connection...");
            server.establishConnection(1302);
            Console.WriteLine("Connected");

            string message;

            while (true)
            {
                Console.WriteLine("Waiting for message...");
                message = server.retrieveMessage();
                Console.WriteLine(message + "\n");

            }

           

        }
    }
}
