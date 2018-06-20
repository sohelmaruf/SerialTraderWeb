using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class UserInfo : TransactionalInformation
    {
        public static string LOGIN_SUCCESSFUL = "Login successful.";
        //public List<applicationmenu> MenuItems;
        //public user User;

        public UserInfo()
        {
            //User = new user();
            //MenuItems = new List<applicationmenu>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
