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

            //Creating a new client
            Client client = new Client();

            //Establishing a connection
            Console.WriteLine("Establishing connection...");
            client.establishConnection("127.0.0.1", 1302);
            Console.WriteLine("Connected");

            string response;
            string message;

            while (true)
            {
                try {
                    Console.WriteLine("Send Message To Server: ");
                    message = Console.ReadLine();

                    response = client.sendMessage(message);
                    Console.WriteLine("Response from server: " + response);

                    //This is handled internally now
                    //client.closeConnection();
                    //client.refreshConnection();
                  
                }
                catch
                {
                    Console.WriteLine("Connection Ended. Please try again");
                }
            }

           

            Console.WriteLine("Enter to continue..");
            Console.ReadLine();
            

        }
    }
}
