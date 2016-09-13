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
        TcpClient transClient;
        string IPAddress;
        int port;
        NetworkStream NetworkStream;

        /**Constructors**/
        public Client(){ }

        /**Methods**/  
        public void establishConnection(string IPAddress = "127.0.0.1", int port = 80)
        {
            
            //Storing connection settings
            this.IPAddress = IPAddress;
            this.port = port;

            try
            {
                //Attemp initial connection
                this.transClient = new TcpClient(IPAddress, port);
                this.NetworkStream = transClient.GetStream();
            }
            catch
            {
                //If initial connection fails attempt to re-connect every 5 seconds
                bool connected = false;

                while(!connected)
                { 
                    Console.WriteLine("Wasn't able to establish connection trying again...");
                    //Waiting 5 seconds before trying again
                    System.Threading.Thread.Sleep(5000);

                    try
                    {
                        this.transClient = new TcpClient(IPAddress, port);
                        this.NetworkStream = transClient.GetStream();
                        //If it makes it this far then it properly connected
                        connected = true;
                    }
                    catch { }
                }


            }

           
        }

        private void refreshConnection()
        {

            //Calling the establishConnection method again with the already provided parameters
            establishConnection(this.IPAddress  , this.port);


        }

        private void closeConnection()
        {
            this.transClient.Close();

        }


        public string sendMessage(string message="")
        {
            string response = "";

            try {

                //Converting message to byte
                byte[] msg = Encoding.Unicode.GetBytes(message);

                //Sending message to server
                NetworkStream.Write(msg, 0, msg.Length);

                //Retrieving message from server
                byte[] buffer = new byte[this.transClient.ReceiveBufferSize];
                int data = NetworkStream.Read(buffer, 0, transClient.ReceiveBufferSize);
                response = Encoding.Unicode.GetString(buffer, 0, data);

                //Refreshing the connection
                closeConnection();
                refreshConnection();

            }
            catch
            {
                //Refreshing the connection
                refreshConnection();
            }

            return response;
        }

    

   
    }
}
