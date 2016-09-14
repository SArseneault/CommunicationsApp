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

            //Add the accepted socket to the list of clients
            clientSockets.Add(clientSocket);

            //Allow the server to accept additional connections again
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);

            //Accepting the incomming connection
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);

        }

        private static void  ReceiveCallBack(IAsyncResult ASResults)
        {
            //Creating a socket for the already accepted connection
            Socket clientSocket = (Socket)ASResults.AsyncState;

            //Storing retrieved message
            int received = clientSocket.EndReceive(ASResults);
            byte[] messageBuf = new byte[received];
            Array.Copy(buffer, messageBuf, received);

            //Converting the message to a string
            string message = Encoding.ASCII.GetString(messageBuf);

            string response = string.Empty;

            //sending the time back to the client
            if(message.ToLower() == "time")
            {
                response = DateTime.Now.ToLongTimeString();
            }
            else
            {
                response = "Thanks";
            }

            //Sending message back to the client
            byte[] data = Encoding.ASCII.GetBytes(response);
            clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);

            //Allow the server to accept additional connections
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);

        }


        private static void SendCallBack(IAsyncResult ASResult)
        {
            Socket clientSocket = (Socket)ASResult.AsyncState;
            clientSocket.EndSend(ASResult);
        }


        public void sendMessage(string message, Socket socket)
        {

            byte[] data = Encoding.ASCII.GetBytes(message);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);

        }


       
    }
}
