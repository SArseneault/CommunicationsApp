using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientApplication;

namespace UnitTestProject
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void CreatingClient()
        {

            //Creating a new client
            Client client = new Client();

            //Establishing a connection
            client.establishConnection();

            client.sendMessage("Test");
           
        }
    }
}
