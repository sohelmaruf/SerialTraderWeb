using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AppLibrary.Model;

namespace Website.Helpers
{
    public class AdminApiModel : TransactionalInformation
    {
        public List<applicationmenu> MenuItems;
        public taccount User;

        public AdminApiModel()
        {
            User = new taccount();
            MenuItems = new List<applicationmenu>();        
        }

    }

    public class UserDTO
    {     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class LoginUserDTO
    {        
        public string UserName { get; set; }
        public string Password { get; set; }      
    }

}