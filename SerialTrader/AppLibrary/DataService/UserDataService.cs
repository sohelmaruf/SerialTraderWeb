using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;

namespace AppLibrary.DataServices
{
    public class UserDataService : EntityFrameworkDataService, IUserDataService
    {
        /// <summary>
        /// Create User
        /// </summary>
        public void AddUser(taccount objUser)
        {
            dbConnection.taccounts.Add(objUser);
        }

        public taccount GetUser(int ID)
        {
            taccount objUser = dbConnection.taccounts.SingleOrDefault(u => u.ACCOUNTID == ID);
            return objUser;
        }
        
        public taccount GetUserByUserName(string userName)
        {
            taccount user = dbConnection.taccounts.SingleOrDefault(u => u.EMAILADDRESS == userName);
            return user;
        }

        public taccount Login(string userName, string password)
        {
            taccount user = dbConnection.taccounts.SingleOrDefault(u => u.EMAILADDRESS == userName && u.PASSWORD == password);
            return user;
        }

        public void UpdateLastLogin(taccount user)
        {
            //user.DateLastLogin = DateTime.Now;
        }

        public void UpdateUser(taccount objUser)
        {
            dbConnection.taccounts.Add(objUser);
        }
    }
}
