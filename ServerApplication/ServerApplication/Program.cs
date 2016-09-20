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

            //Creating new server object -*This port number should be stored in config file.
            Server server = new Server(1302);

            //Setting up the connection
            Console.WriteLine("Establishing Connection...");
            server.establishConnection();
            Console.WriteLine("Connected");


            Console.ReadKey();

        }
    }
}
