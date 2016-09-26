using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerApplication
{
    class Server
    {
        /**Fields**/
        private static byte[] buffer; //Used for sending and receiving messages
        private static List<Socket> clientSockets; //List of client sockets
        private static Socket serverSocket;
        private int port;

        /**Constructors**/
        public Server(int port = 1302)
        {
            //Initialing the fields
            serverSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
            clientSockets = new List<Socket>();
            buffer = new byte[1024];
            
            
            Console.Title = "ServerSide";

        }

        /**Methods**/
        public void establishConnection()
        {

            //Binding on all available interfaces on provided port
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));

            //Listening to client. Backlog = 10 pending connections
            //All connections will be refused after
            serverSocket.Listen(10);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
        }


        private static void AcceptCallBack(IAsyncResult ASResults)
        {
            //Retrieve accepted connection and create a new client socket
            Socket clientSocket = serverSocket.EndAccept(ASResults);

            try {
                //Add the accepted socket to the list of clients
                clientSockets.Add(clientSocket);

                //Allow the server to accept additional connections again
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);

                //Accepting the incomming connection
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
            }
            catch
            {
                disconnectClient(clientSocket);
            }

        }

        private static void  ReceiveCallBack(IAsyncResult ASResults)
        {
            //Creating a socket for the already accepted connection
            Socket clientSocket = (Socket)ASResults.AsyncState;

            try {
                //Storing retrieved message
                int received = clientSocket.EndReceive(ASResults);
                byte[] messageBuf = new byte[received];
                Array.Copy(buffer, messageBuf, received);

                //Converting the message to a string
                string message = Encoding.ASCII.GetString(messageBuf);

                //Reteriving client IP address
                IPEndPoint remoteIpEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
                IPEndPoint localIpEndPoint = clientSocket.LocalEndPoint as IPEndPoint;



                //if (remoteIpEndPoint != null)
                //{
                //    // Using the RemoteEndPoint property.
                //    Console.WriteLine("I am connected to " + remoteIpEndPoint.Address + "on port number " + remoteIpEndPoint.Port);
                //}

                //if (localIpEndPoint != null)
                //{
                //    // Using the LocalEndPoint property.
                //    Console.WriteLine("My local IpAddress is :" + localIpEndPoint.Address + "I am connected on port number " + localIpEndPoint.Port);
                //}

                //Displaying the message to console
                Console.WriteLine("Request from client " + remoteIpEndPoint.Address + ": " + message);

                //Determingin what to do with the request
                string response = determineRequest(message);


                //Sending message back to the client
                byte[] data = Encoding.ASCII.GetBytes(response);
                clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);

                //Allow the server to accept additional connections
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);
            }
            catch
            {
                disconnectClient(clientSocket);
            }
        }

        private static string determineRequest(string message)
        {

            string response = string.Empty;

            //Update coordinates


            //sending the time back to the client
            if (message.ToLower() == "time")
            {
                response = DateTime.Now.ToLongTimeString();
            }
            else if (message.ToLower() == "broadcast")
            {
                broadcastToClients("Hello ALL");
            }
            else
            {
                response = "Thanks";
            }

            return response;

        }

        
        //Broadcasting message to ALL clients
        private static void broadcastToClients(string message, Socket sourceClient = null)
        {
            //Creaitng  a list of sockets to store the "bad" connections. These will later be disconnected.
            List<Socket> disconnectedSockets = new List<Socket>();

            //Looping through the client socket list
            foreach (Socket clientSocket in clientSockets)
            {
                if (clientSocket != sourceClient)
                {
                    try
                    {
                        //Sending message to client
                        byte[] data = Encoding.ASCII.GetBytes(message);
                        clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);

                        //Allow the server to accept additional connections
                        clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);

                    }
                    catch
                    {
                        //Adding to the list of sockets to be properly disconnected.
                        disconnectedSockets.Add(clientSocket);
                    }
                }
             }

            //Properly disconnecting the clients from the server
            foreach(Socket s in disconnectedSockets)
            {
                disconnectClient(s);
            }

        }


        private static void SendCallBack(IAsyncResult ASResult)
        {
            Socket clientSocket = (Socket)ASResult.AsyncState;
            clientSocket.EndSend(ASResult);
        }


        public void sendMessage(string message, Socket socket)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
            }
            catch
            {
                disconnectClient(socket);
            }

        }

        private static void disconnectClient(Socket clientSocket)
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

                //Grabbing IP from client
                IPEndPoint remoteIpEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
             
                //Updating rest of clients 
                broadcastToClients("IP Disconnected: " + remoteIpEndPoint.Address +" TEST: " + remoteIpEndPoint.ToString());

            }
            catch
            {
                //Disconnect didn't happen
            }
        }


       
    }
}
