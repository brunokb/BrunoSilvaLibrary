using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace BrunoSilvaLibrary.Models
{

    public interface UserData
    {
        UserModel ValidPassword(string username, string password);
        bool CreateUser(string username, string password, string email);
        bool UpdateUser(string username, string password, string email, int userlevel);
        bool DeleteUser(string username);

    }
}