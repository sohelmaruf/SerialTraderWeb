using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;
using AppLibrary.DataServices;

namespace AppLibrary.Business
{
    public class AdminBusinessRules : ValidationRules
    {

        IAdminDataService adminDataService;

        /// <summary>
        /// Initialize User Business Rules
        /// </summary>
        /// <param name="objUser"></param>
        /// <param name="dataService"></param>
        public void InitializeAdminBusinessRules(taccount objUser, IAdminDataService dataService)
        {
            adminDataService = dataService;
            InitializeValidationRules(objUser);
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void ValidateUser(taccount user, IAdminDataService dataService)
        {
            adminDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");           
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");              

            ValidateUniqueUserName(user.EMAILADDRESS);
        }

        public void ValidateRegistration(RegisterInfo objRegisterInfo, IAdminDataService dataService)
        {
            adminDataService = dataService;

            InitializeValidationRules(objRegisterInfo);

            ValidateRequired("Title", "Organization Name");
            ValidateRequired("ContactPerson", "Contact Person Name");
            ValidateRequired("Email", "Email Address");
            ValidateRequired("UserName", "UserName");
            ValidateRequired("Password", "Password");

            ValidateEmailAddress("Email", "Email Address");
            ValidateUniqueOrganization(objRegisterInfo.Title);
            ValidateUniqueUserName(objRegisterInfo.UserName);
            ValidatePassword(objRegisterInfo.Password, objRegisterInfo.PasswordConfirmation);
        }

        public void ValidateUniqueOrganization(string title)
        {
            //IClientDataService dService = new ClientDataService();
            //if (!string.IsNullOrEmpty(title))
            //{
            //    client objCLient = dService.GetClientByTitle(title);
            //    if (objCLient != null)
            //    {
            //        AddValidationError("Title", "- Organization '" + title + "' already exists. Please ask your account admin to create a user for you.");
            //    }
            //}
        }

        public void ValidateExistingUser(taccount user, IAdminDataService dataService)
        {
            adminDataService = dataService;

            InitializeValidationRules(user);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("UserName", "User Name");
            ValidateRequired("Password", "Password");
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");

            //ValidateUniqueUserNameForExistingUser(user.ID, user.UserName);

        }


        /// <summary>
        /// Validate Unique User Name
        /// </summary>
        /// <param name="userName"></param>
        public void ValidateUniqueUserName(string userName)
        {

            taccount user = adminDataService.GetUserByUserName(userName);
            if (user != null)
            {
                AddValidationError("UserName", "- User '" + userName + "' already exists. Please choose a different User Name.");
            }

        }

        /// <summary>
        /// Validate Unique User Name for Existing User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        public void ValidateUniqueUserNameForExistingUser(int userID, string userName)
        {
            taccount user = adminDataService.GetUserByUserName(userName);
            if (user != null)
            {
              //if (user.ID != userID)
              //{
              //  AddValidationError("UserName", "- User '" + userName + "' already exists. Please choose a different User Name");
              //}
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
