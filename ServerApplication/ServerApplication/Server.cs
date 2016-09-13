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
        //Old Variables
        //StreamReader sr;
        //StreamWriter sw;

        /**Constructors**/
        public Server() { }

        /**Methods**/
        public void establishConnection(int port = 1302)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
            this.listener.Start();

            /*Old TCP listener
            this.listener = new TcpListener(port);
            this.listener.Start();
            */
        }

        public string retrieveMessage()
        {

            //Refreshing the stream
            this.client = listener.AcceptTcpClient();

            NetworkStream = this.client.GetStream();
            //sr = new StreamReader(client.GetStream());
            //sw = new StreamWriter(client.GetStream());
            //string message = sr.ReadLine();

            byte[] buffer = new byte[this.client.ReceiveBufferSize];
            int data = NetworkStream.Read(buffer, 0, client.ReceiveBufferSize);
            string message = Encoding.Unicode.GetString(buffer, 0, data);

     
            return message;
        }

        public void closeConnection()
        {
            client.Close();
        }
    }
}
