using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace ClientApplication
{
    public class Client
    {
        /**Fields**/
        private static Socket clientSocket;
        private int port;

        /**Constructors**/
        public Client(int port = 1302)
        {
            //Initializing fields
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.port = port;

            Console.Title = "ClientSide";
        }

        /**Methods**/
        public void establishConnection()
        {
            //Creating counter for connection attempts
            int connectionAttempts = 0;

            //Attempting to connect to server over loop
            while (!clientSocket.Connected)
            {
                try
                {
                    //Incrementing attempt counter
                    connectionAttempts++;

                    //Create connection
                    clientSocket.Connect(IPAddress.Loopback, port);
                }
                catch (SocketException)
                {
                    //Writing the connection attempts to the user
                    Console.Clear();
                    Console.WriteLine("Current Connection Attempt #: " + connectionAttempts.ToString());
                }

            }

            Console.Clear();
            Console.WriteLine("Connected");
           
        }

    

        public string sendMessage(string message="")
        {
            string response = string.Empty;

            try {
                //Converting message to a byte
                byte[] messageBuffer = Encoding.ASCII.GetBytes(message);

                //Sending message to server
                clientSocket.Send(messageBuffer);

                //Retreive message from server
                byte[] responseBuffer = new byte[1024];
                int responseSize = clientSocket.Receive(responseBuffer);

                //Converting response to a byte array
                byte[] data = new byte[responseSize];
                Array.Copy(responseBuffer, data, responseSize);

                //Converting the message to a string
                response = Encoding.ASCII.GetString(responseBuffer);

                //Removing trailing null characters
                response.Replace("\n", String.Empty).Trim();
            }
            catch
            {

                Console.WriteLine("Lost Connection to Server");
                disconnectServer();
                establishConnection();
            }

            return response;
        }

        private void disconnectServer()
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch
            {
                //Disconnect didn't happen
            }
        }

   
    }
}
