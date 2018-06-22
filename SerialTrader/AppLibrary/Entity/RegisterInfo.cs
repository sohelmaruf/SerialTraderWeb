using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Common;
using AppLibrary.Model;

namespace AppLibrary.Entity
{
    public class RegisterInfo : TransactionalInformation
    {
        public const string CONTACT_USER_ADD = "Contact user added successfully.";
        public List<applicationmenu> MenuItems;
        public taccount User;

        public RegisterInfo()
        {
            User = new taccount();
            MenuItems = new List<applicationmenu>();
        }

        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        public string DefaultPage { get; set; }
        public string SubscriptionType { get; set; }
        public Nullable<int> PackageID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public Nullable<int> ContactID { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Logo { get; set; }
        public string Role { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> TermsAccepted { get; set; }
        
    }
}
