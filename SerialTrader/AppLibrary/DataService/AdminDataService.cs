using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.DataServices;

namespace AppLibrary.DataServices
{
    public class AdminDataService : EntityFrameworkDataService, IAdminDataService
  {
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void RegisterUser(taccount user)
        {
            DateTime now = DateTime.Now;
        
            //user.ID = Guid.NewGuid();          
            //user.DateCreated = now;
            //user.DateLastLogin = now;
            //user.DateUpdated = now;

            dbConnection.taccounts.Add(user);       
            
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(taccount user)
        {
            DateTime now = DateTime.Now;           
            //user.DateUpdated = now;          

        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public taccount GetUserByUserName(string userName)
        {
            taccount user = dbConnection.taccounts.SingleOrDefault(u => u.EMAILADDRESS == userName);
            return user;
        }


        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public taccount GetUser(int userID)
        {
            taccount user = dbConnection.taccounts.SingleOrDefault(u => u.ACCOUNTID == userID);
            return user;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public taccount Login(string userName, string password)
        {
            taccount user = dbConnection.taccounts.SingleOrDefault(u => u.EMAILADDRESS == userName && u.PASSWORD == password);
            return user;
        }

        public void UpdateLastLogin(taccount user)
        {
            //user.DateLastLogin = DateTime.Now;
        }

    }
}
