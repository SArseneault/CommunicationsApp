using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creating a new client - *This port number should be stored in config file.
            Client client = new Client(1302);

            //Establishing a connection
            Console.WriteLine("Establishing connection...");
            client.establishConnection();
           

            string message = String.Empty;
            string response = String.Empty;

            while(true)
            {
                Console.WriteLine("Send message to server:");
                message = Console.ReadLine();
         
                response = client.sendMessage(message);

                Console.WriteLine("Server response: "+response);
        
            }
        }
    }
}
