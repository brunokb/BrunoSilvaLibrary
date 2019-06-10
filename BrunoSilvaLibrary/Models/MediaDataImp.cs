using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrunoSilvaLibrary.Models.Extended
{
    public class MediaDataImp : MediaData
    {
        public List <Media> GetMedias(string title,string genre,string director)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";
            Media varMedia = new Media();
            List<Media> listMedia= new List<Media>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT TabMedia.Title, TabMedia.PublishYear, TabMedia.Budget, TabGenre.GenreName, TabDirector.DirectorName, TabLanguage.LanguageName " +
                        "FROM TabGenre " +
                        "INNER JOIN TabMedia ON TabGenre.GID = TabMedia.Genre " +
                        "INNER JOIN TabDirector ON TabMedia.Director = TabDirector.DID " +
                        "INNER JOIN TabLanguage ON TabMedia.Language = TabLanguage.LID " +
                        "WHERE(TabDirector.DirectorName like IsNull("+ director +", '%')) AND(TabMedia.Title like IsNull(" + title + ", '%')) AND(TabGenre.GenreName like IsNull(" + genre + ", '%'))";
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            varMedia.Director = reader.GetString(0);
                            varMedia.Budget = reader.GetString(1);
                            varMedia.Genre = reader.GetString(2);
                            varMedia.Language = reader.GetString(3);
                            listMedia.Add(varMedia);
                        }
                        return listMedia;
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
            return listMedia;
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