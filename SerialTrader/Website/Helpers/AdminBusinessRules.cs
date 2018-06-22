using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Website.Helpers;
using AppLibrary.Model;


namespace Website.Helpers
{
    class AdminBusinessRules : ValidationRules
    {
    IAdminDataService accountsDataService;

        /// <summary>
        /// Initialize user Business Rules
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void InitializeAccountsBusinessRules(taccount user, IAdminDataService dataService)
        {
            accountsDataService = dataService;
            InitializeValidationRules(user);
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void ValidateUser(taccount user, IAdminDataService dataService)
        {
            accountsDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");           
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");              

            ValidateUniqueUserName(user.EMAILADDRESS);

    
        }

        public void ValidateExistingUser(taccount user, IAdminDataService dataService)
        {
            accountsDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");

            //ValidateUniqueUserNameForExistingUser(user.ACCOUNTID, user.EMAILADDRESS);

        }


        /// <summary>
        /// Validate Unique User Name
        /// </summary>
        /// <param name="userName"></param>
        public void ValidateUniqueUserName(string userName)
        {

            taccount user = accountsDataService.GetUserByUserName(userName);
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
            taccount user = accountsDataService.GetUserByUserName(userName);
            if (user != null)
            {
              if (user.ACCOUNTID != userID)
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
