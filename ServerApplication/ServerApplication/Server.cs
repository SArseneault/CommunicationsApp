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
        TcpListener listener;
        TcpClient client;
        NetworkStream NetworkStream;
        /**Constructors**/
        public Server() { }

        /**Methods**/
        public void establishConnection(int port = 1302)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
            this.listener.Start();

        }

        public string retrieveMessage()
        {

            //Refreshing the stream
            this.client = listener.AcceptTcpClient();
            NetworkStream = this.client.GetStream();

            //Reteriving message from client
            byte[] buffer = new byte[this.client.ReceiveBufferSize];
            int data = NetworkStream.Read(buffer, 0, client.ReceiveBufferSize);
            string message = Encoding.Unicode.GetString(buffer, 0, data);

            //Sending confirmation message back to client
            sendMessage("Message received");

            //Refreshing the connection
            closeConnection();    

            return message;
        }

        public void sendMessage(string message)
        {

            //Converting message to byte
            byte[] msg = Encoding.Unicode.GetBytes(message);

            //Sending message to server
            NetworkStream.Write(msg, 0, msg.Length);

        }

        private void closeConnection()
        {
            client.Close();
        }

       
    }
}
