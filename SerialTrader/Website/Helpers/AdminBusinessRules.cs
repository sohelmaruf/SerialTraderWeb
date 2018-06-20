using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WaterCons.Helpers;
using WaterCons.Models;


namespace WaterCons.Helpers
{
    class AdminBusinessRules : ValidationRules
    {
    IAdminDataService accountsDataService;

        /// <summary>
        /// Initialize user Business Rules
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void InitializeAccountsBusinessRules(user user, IAdminDataService dataService)
        {
            accountsDataService = dataService;
            InitializeValidationRules(user);
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void ValidateUser(user user, IAdminDataService dataService)
        {
            accountsDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");           
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");              

            ValidateUniqueUserName(user.UserName);

    
        }

        public void ValidateExistingUser(user user, IAdminDataService dataService)
        {
            accountsDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");

            ValidateUniqueUserNameForExistingUser(user.ID, user.UserName);

        }


        /// <summary>
        /// Validate Unique User Name
        /// </summary>
        /// <param name="userName"></param>
        public void ValidateUniqueUserName(string userName)
        {

            user user = accountsDataService.GetUserByUserName(userName);
            if (user != null)
            {
                AddValidationError("UserName", "User Name " + userName + " already exists.");
            }

        }

        /// <summary>
        /// Validate Unique User Name for Existing User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        public void ValidateUniqueUserNameForExistingUser(int userID, string userName)
        {
            user user = accountsDataService.GetUserByUserName(userName);
            if (user != null)
            {
              if (user.ID != userID)
              {
                AddValidationError("UserName", "User Name " + userName + " already exists.");
              }
            }

        }


        /// <summary>
        /// Password Confirmation Failed
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordConfirmation"></param>
        public void ValidatePassword(string password, string passwordConfirmation)
        {

            if (passwordConfirmation.Length==0)
                AddValidationError("PasswordConfirmation", "Password confirmation required.");

            if (password != passwordConfirmation)
            {
                AddValidationError("PasswordConfirmation", "Password confirmation failed.");
            }

        }


    }
}
