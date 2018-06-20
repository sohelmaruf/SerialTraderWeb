using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WaterCons.Helpers;
using WaterCons.Models;

namespace WaterCons.Helpers
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
        public void RegisterUser(user user)
        {
            DateTime now = DateTime.Now;
        
            //user.ID = Guid.NewGuid();          
            user.DateCreated = now;
            user.DateLastLogin = now;
            user.DateUpdated = now;

            dbConnection.users.Add(user);       
            
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(user user)
        {
            DateTime now = DateTime.Now;           
            user.DateUpdated = now;          

        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public user GetUserByUserName(string userName)
        {
            user user = dbConnection.users.SingleOrDefault(u => u.UserName == userName);
            return user;
        }


        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public user GetUser(int userID)
        {
            user user = dbConnection.users.SingleOrDefault(u => u.ID == userID);
            return user;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public user Login(string userName, string password)
        {
            user user = dbConnection.users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }

        public void UpdateLastLogin(user user)
        {
            user.DateLastLogin = DateTime.Now;
        }

    }
}
