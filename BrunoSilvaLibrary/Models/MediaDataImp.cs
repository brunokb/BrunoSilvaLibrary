using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrunoSilvaLibrary.Models.Extended
{
    public class MediaDataImp : MediaData
    {
        public List <Media> GetMedias(string title,string genre,string director)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";
            List<Media> listMedia= new List<Media>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT TabMedia.MediaID, TabMedia.Title, TabMedia.PublishYear, TabMedia.Budget, TabGenre.GenreName, TabDirector.DirectorName, TabLanguage.LanguageName " +
                        "FROM TabGenre " +
                        "INNER JOIN TabMedia ON TabGenre.GID = TabMedia.Genre " +
                        "INNER JOIN TabDirector ON TabMedia.Director = TabDirector.DID " +
                        "INNER JOIN TabLanguage ON TabMedia.Language = TabLanguage.LID " +
                        "WHERE(TabDirector.DirectorName like IsNull("+ ((director == null) ? "NULL" : ("'"+director+"%'")) +", '%')) AND(TabMedia.Title like IsNull(" + ((title == null) ? "NULL" : ("'"+title + "%'")) + ", '%')) AND(TabGenre.GenreName like IsNull(" + ((genre == null) ? "NULL" : ("'"+genre + "%'")) + ", '%'))";
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Media varMedia = new Media();
                            varMedia.MID = reader.GetInt32(0);
                            varMedia.Title = reader.GetString(1);
                            varMedia.PublishYear = reader.GetInt32(2);
                            varMedia.Budget = reader.GetDecimal(3);
                            varMedia.Genre = reader.GetString(4);
                            varMedia.Director = reader.GetString(5);
                            varMedia.Language = reader.GetString(6);
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
        public bool CreateMedia(string title,string genre, string director, string language, string publishYear, string budget)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";
            int GID =0, DID=0, LID=0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT Title FROM TabMedia WHERE Title like '" + title + "'";
                    SqlDataReader rTitle = selectCommand.ExecuteReader();
                    if (rTitle.HasRows)
                    {
                        rTitle.Close();
                        return false;
                        //MSG - title already exist
                    }
                    else
                    {
                        // ------------------------ Check GENRE and create -----------------------------//
                    rTitle.Close();
                    selectCommand.CommandText = "SELECT GID FROM TabGenre WHERE GenreName like '"+ genre +"'";
                    SqlDataReader rGenre = selectCommand.ExecuteReader();
                    if (rGenre.HasRows)
                    {
                        while(rGenre.Read()) GID = rGenre.GetInt32(0);
                    }
                    else
                    {
                        selectCommand.CommandText = "INSERT INTO TabGenre (GenreName) VALUES ('" + genre + "')";
                        selectCommand.ExecuteReader();
                        selectCommand.CommandText = "SELECT GID FROM TabGenre WHERE GenreName like'" + genre + "'";
                        rGenre = selectCommand.ExecuteReader();
                        while (rGenre.Read()) GID = rGenre.GetInt32(0);

                        }

                    rGenre.Close();
                    // ------------------------ Check DIRECTOR and create -----------------------------//
                    selectCommand.CommandText = "SELECT DID FROM dbo.TabDirector WHERE DirectorName like '" + director + "'";
                    SqlDataReader rDirector = selectCommand.ExecuteReader();
                    if (rDirector.HasRows)
                    {
                            while (rDirector.Read()) DID = rDirector.GetInt32(0);
                    }
                    else
                    {
                    rDirector.Close();
                    selectCommand.CommandText = "INSERT INTO TabDirector (DirectorName) VALUES ('" + director + "')";
                    selectCommand.ExecuteReader();
                    selectCommand.CommandText = "SELECT DID FROM dbo.TabDirector WHERE DirectorName like '" + director + "'";
                    SqlDataReader r2Director = selectCommand.ExecuteReader();

                        while (r2Director.Read()) DID = rDirector.GetInt32(0);
                            r2Director.Close();
                        }
                        rDirector.Close();


                    // ------------------------ Check LANGUAGE and create -----------------------------//
                        selectCommand.CommandText = "SELECT LID FROM dbo.TabLanguage WHERE LanguageName like '" + language + "'";
                    SqlDataReader rLanguage = selectCommand.ExecuteReader();
                    if (rLanguage.HasRows)
                    {
                            while (rLanguage.Read()) LID = rLanguage.GetInt32(0);
                    }
                    else
                    {
                        selectCommand.CommandText = "INSERT INTO TabLanguage (LanguageName) VALUES ('" + language + "')";
                        selectCommand.ExecuteReader();
                        selectCommand.CommandText = "SELECT LID FROM dbo.TabLanguage WHERE LanguageName like '" + language + "'";
                        rLanguage = selectCommand.ExecuteReader();
                            while (rLanguage.Read()) LID = rLanguage.GetInt32(0);
                    }
                    rLanguage.Close();
                    // ------------------------ Check TITLE and create -----------------------------//
                        selectCommand.CommandText = "INSERT INTO TabMedia (Title,Genre,Director,Language,PublishYear,Budget) VALUES ('" + title + "'," + System.Convert.ToInt32(GID) + ",'" + System.Convert.ToInt32(DID) + "','" + System.Convert.ToInt32(LID) + "','" + publishYear + "','" + System.Convert.ToDecimal(budget) + "')";
                        selectCommand.ExecuteReader();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool UpdateMedia(int MID, string title, string genre, string director, string language, string publishYear, string budget)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "UPDATE dbo.TabMedia SET (Title = '" + title + "',Genre = '" + genre + "', ,Director = '" + director + "',Language =" + language + "',PublishYear = "+ publishYear + "',Budget ="+ budget+ "') WHERE MediaID = '" + MID + "'";
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
        public bool DeleteMedia(int MID)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "DELETE dbo.TabMedia WHERE MediaID = '" + MID + "'";
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
        public bool MediaBorrow(int MID, int UID, DateTime bDate, DateTime rDate, int lateFee)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "INSERT INTO TabBorrow(UID, MediaID, BorrowDate, ReturnDate, ActualReturnDate, LateFee) VALUES('" + UID + "', " + MID + ", '" + bDate + "', '" + rDate + "', '" + rDate + "', '" + lateFee + "')";
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
        public bool MediaAvailable(int MID, DateTime bDate)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "SELECT * FROM TabBorrow WHERE MediaID =" + MID + " AND BorrowDate <= '"+bDate+"' AND ReturnDate >='"+bDate+"'";
                    SqlDataReader mediaCheck = selectCommand.ExecuteReader();
                    if (mediaCheck.HasRows)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool MediaReserve(int MID, int UID, DateTime bDate)
        {
            string conString = "Data Source = SQL5020.site4now.net; Initial Catalog = DB_9AB8B7_B19ES6931; User ID = DB_9AB8B7_B19ES6931_admin; Password=z9jjQg9H";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();
                    SqlCommand selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = "INSERT INTO TabBorrow(UID, MediaID, BorrowDate) VALUES('" + UID + "', " + MID + ", '" + bDate +"')";
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