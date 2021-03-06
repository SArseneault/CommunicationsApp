﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ServerApplication
{
    public class Database
    {
        /*Fields*/
        MySql.Data.MySqlClient.MySqlConnection dbConn; //Stores the connectionstring of the database
        MySql.Data.MySqlClient.MySqlCommand cmd; //What we will be acting on after the db is instantiated
        string connString;

        /*Constructors*/
        public Database()
        {
            this.connString = System.Configuration.ConfigurationManager.ConnectionStrings["AdminDatabaseConnection"].ToString();
            // connString = "server = localhost; user id=root; password=Minusbears20#;";

            this.dbConn = new MySql.Data.MySqlClient.MySqlConnection(connString);
            
        }
        /*Methods*/
        public bool InsertNewUser(string username, string password, string email)
        {
            try
            {
                cmd = new MySqlCommand("InsertNewUser", dbConn);
                   
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters["@Username"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters["@Password"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Salt", "");
                cmd.Parameters["@Salt"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@SaltP", "");
                cmd.Parameters["@SaltP"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Role", 1);
                cmd.Parameters["@Role"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;

                //Performing the actual execution
                dbConn.Open();
                cmd.ExecuteNonQuery();
                dbConn.Close();
                    
                

                return true;
            }
            catch
            {

                dbConn.Close();
                return false;

            }

        }

        public bool LoginUser(string username, string password)
        {
            try
            {
                cmd = new MySqlCommand("LoginUser", dbConn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UN", username);
                cmd.Parameters["@UN"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@PASS", password);
                cmd.Parameters["@PASS"].Direction = System.Data.ParameterDirection.Input;

                cmd.Parameters.Add(new MySqlParameter("UserExists", MySqlDbType.VarChar));
                cmd.Parameters["UserExists"].Direction = ParameterDirection.Output;

                

                //Performing the actual execution
                dbConn.Open();
                cmd.ExecuteNonQuery();

                string userExist = cmd.Parameters["UserExists"].Value.ToString();

                dbConn.Close();


                if (string.IsNullOrEmpty(userExist))
                {
                    return false;
                } 
                
                //User was logged in
                return true;
            }
            catch
            {
                dbConn.Close();

                //Wasn't able to login the user
                return false;

            }

        }



        //Basic sql operations to be implemented (if ever)
        public string SELECT(string table, string username) { return ""; }
        public string INSERT() { return ""; }
        public string DELETE() { return ""; }
        public string TRUNCATE() { return ""; }
    }
}
