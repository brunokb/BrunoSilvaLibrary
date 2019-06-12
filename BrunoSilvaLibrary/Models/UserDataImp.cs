using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrunoSilvaLibrary.Models
{
    public class UserDataImp : UserData
    {
        public UserModel ValidPassword(string username, string password)
        {
            UserModel validUser = null;


            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT UID, UserEmail, UserLevel FROM dbo.TabUser WHERE UserName='" + username + "' AND Password='" + password + "';";
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                                reader.GetString(1));

                            validUser = new UserModel();
                            validUser.UID = reader.GetInt32(0);
                            validUser.UserEmail = reader.GetString(1);
                            validUser.UserLevel = reader.GetInt32(2);
                            validUser.UserName = username;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return validUser;
        }

        public List<UserModel> GetUserList()
        {
            List<UserModel> listUsers = new List<UserModel>();
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT UID, UserEmail, UserLevel, UserName FROM dbo.TabUser";
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel validUser = new UserModel();
                            validUser.UID = reader.GetInt32(0);
                            validUser.UserEmail = reader.GetString(1);
                            validUser.UserLevel = reader.GetInt32(2);
                            validUser.UserName = reader.GetString(3);
                            listUsers.Add(validUser);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return listUsers;
        }
        public bool CreateUser(string username, string password, string email)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "INSERT INTO dbo.TabUser (UserName,Password,UserLevel,UserEmail) VALUES ('" + username + "', '" + password + "' , 1 ,'" + email + "')";
                    selectCommand.ExecuteReader();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool UpdateUser(string username, string password, string email, int userlevel)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "UPDATE dbo.TabUser SET Password = '" + password + "', UserLevel = '" + userlevel + "', UserEmail = '" + email + "' WHERE UserName = '" + username + "'";
                    selectCommand.ExecuteReader();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool DeleteUser(string username)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "DELETE dbo.TabUser WHERE UserName = '" + username + "'";
                    selectCommand.ExecuteReader();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}