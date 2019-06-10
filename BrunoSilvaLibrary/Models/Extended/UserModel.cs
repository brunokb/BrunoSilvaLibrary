using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BrunoSilvaLibrary.Models
{
    public class UserModel
    {
        public int UID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserLevel { get; set; }
        public string UserEmail { get; set; }
        public string ConfirmPassword { get; set; }

    }
}