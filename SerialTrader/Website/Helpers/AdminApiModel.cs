using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WaterCons.Models;

namespace WaterCons.Helpers
{
    public class AdminApiModel : TransactionalInformation
    {
        public List<applicationmenu> MenuItems;
        public user User;

        public AdminApiModel()
        {
            User = new user();
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