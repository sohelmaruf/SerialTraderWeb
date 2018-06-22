using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.DataServices;


namespace AppLibrary.Business
{
    public class UserBusinessRules : ValidationRules
    {

        IUserDataService userDataService;

        /// <summary>
        /// Initialize User Business Rules
        /// </summary>
        /// <param name="objUser"></param>
        /// <param name="dataService"></param>
        public void InitializeUserBusinessRules(taccount objUser, IUserDataService dataService)
        {
            userDataService = dataService;
            InitializeValidationRules(objUser);
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void ValidateUser(taccount user, IUserDataService dataService)
        {
            userDataService = dataService;

            InitializeValidationRules(user);
            
            ValidateRequired("UserName", "UserName");
            ValidateRequired("Password", "Password");                     

            ValidateUniqueUserName(user.EMAILADDRESS);
        }

        public void ValidateExistingUser(taccount user, IUserDataService dataService)
        {
            userDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");

            //ValidateUniqueUserNameForExistingUser(user.EMAILADDRESS, user.EMAILADDRESS);

        }


        /// <summary>
        /// Validate Unique User Name
        /// </summary>
        /// <param name="userName"></param>
        public void ValidateUniqueUserName(string userName)
        {
            IUserDataService dService = new UserDataService();
            if (!string.IsNullOrEmpty(userName))
            {
                taccount user = dService.GetUserByUserName(userName);
                if (user != null)
                {
                    AddValidationError("UserName", "- User '" + userName + "' already exists. Please choose a different User Name.");
                }
            }
        }

        /// <summary>
        /// Validate Unique User Name for Existing User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        public void ValidateUniqueUserNameForExistingUser(int userID, string userName)
        {
            taccount user = userDataService.GetUserByUserName(userName);
            if (user != null)
            {
              //if (user.EMAILADDRESS != userID)
              //{
              //      AddValidationError("UserName", "- User '" + userName + "' already exists. Please choose a different User Name.");
              //  }
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
                AddValidationError("PasswordConfirmation", "- Password confirmation required.");

            if (password != passwordConfirmation)
            {
                AddValidationError("PasswordConfirmation", "- Password confirmation failed.");
            }

        }

    }
}
