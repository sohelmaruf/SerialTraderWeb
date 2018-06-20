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
        public void AddUser(user objUser)
        {
            dbConnection.users.Add(objUser);
        }

        public user GetUser(int ID)
        {
            user objUser= dbConnection.users.SingleOrDefault(u => u.ID == ID);
            return objUser;
        }
        
        public user GetUserByUserName(string userName)
        {
            user user = dbConnection.users.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

        public user Login(string userName, string password)
        {
            user user = dbConnection.users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }

        public void UpdateLastLogin(user user)
        {
            user.DateLastLogin = DateTime.Now;
        }

        public void UpdateUser(user objUser)
        {
            dbConnection.users.Add(objUser);
        }
    }
}
