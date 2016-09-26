using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApplication;

namespace UnitTestProject
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void TestMethod1()
        {

           Database db = new Database();

            db.InsertNewUser("Sam2", "test", "S@yahoo.oom");

        }
    }
}
