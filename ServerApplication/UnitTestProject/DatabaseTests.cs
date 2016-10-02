using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApplication;

namespace UnitTestProject
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CreatingNewUser()
        {

           Database db = new Database();

            db.InsertNewUser("Sam2", "test", "S@yahoo.oom");


        }

        [TestMethod]
        public void LoginExistingUser()
        {

            Database db = new Database();

            db.LoginUser("Sam2", "test");


        }
    }
}
