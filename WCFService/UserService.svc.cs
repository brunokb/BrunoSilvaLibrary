using BrunoSilvaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BrunoSilvaLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        List<UserModel> IUserService.UserReturn()
        {
            List <UserModel> umd = new List <UserModel>();
            UserDataImp usrImp = new UserDataImp();
            umd = usrImp.GetUserList();
            return umd;
        }
    }
}
