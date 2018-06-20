using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

using WaterCons.Helpers;
using WaterCons.Filters;
using WaterCons.Library.Interfaces;
using WaterCons.Library.DataServices;
using WaterCons.Library.Business;
using WaterCons.Library.Entity;
using WaterCons.Library.Models;
using WaterCons.Library.Common;


namespace WaterCons.Controllers
{

  [RoutePrefix("api/admin")]
  public class AdminAPIController : ApiController
    {
       private waterconsEntities db = new waterconsEntities();

        IAdminDataService adminDataService;
        IApplicationDataService applicationDataService;
        IUserDataService userDataService;
        IClientDataService clientDataService;

        public AdminAPIController()
        {
            adminDataService = new AdminDataService();
            applicationDataService = new ApplicationDataService();
            userDataService = new UserDataService();
            clientDataService = new ClientDataService();
        }


        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="registerUserDTO"></param>
        /// <returns></returns>
        [Route("RegisterClient")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage RegisterClient(HttpRequestMessage request, [FromBody] RegisterInfo objRegisterInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();   
            ClientBusinessService clientBusinessService = new ClientBusinessService(clientDataService);
            AdminBusinessRules adminRules = new AdminBusinessRules();

            if (objRegisterInfo.ContactPerson != null)
            {
                if (objRegisterInfo.ContactPerson.Contains(","))
                {
                    string[] Names = objRegisterInfo.ContactPerson.Split(',');
                    objRegisterInfo.FirstName = Names[1].ToString();
                    objRegisterInfo.LastName = Names[0].ToString();
                }
                else
                {
                    objRegisterInfo.FirstName = "";
                    objRegisterInfo.LastName = objRegisterInfo.ContactPerson;
                }
            }
            objRegisterInfo.IsActive = true;
            objRegisterInfo.Role = Constants.WATERCONS_ROLE_ADMIN;
            objRegisterInfo.DefaultPage = Constants.WATERCONS_APPLICATION_DEFAULT_PAGE;

            adminRules.ValidateRegistration(objRegisterInfo, adminDataService);

            if (adminRules.ValidationStatus == true)
            {
                client objclient = clientBusinessService.AddClient(
                       objRegisterInfo.SubscriptionType,
                       objRegisterInfo.Title,
                       objRegisterInfo.Code,
                       objRegisterInfo.Address,
                       objRegisterInfo.ContactID,
                       objRegisterInfo.ContactPerson,
                       objRegisterInfo.ContactNumber,
                       objRegisterInfo.Logo,
                       objRegisterInfo.Email,
                       objRegisterInfo.IsActive,
                       objRegisterInfo.TermsAccepted,
                       out transaction
                       );

                if (transaction.ReturnStatus == false)
                {
                    objRegisterInfo.ReturnMessage = transaction.ReturnMessage;
                    objRegisterInfo.ReturnStatus = transaction.ReturnStatus;
                    objRegisterInfo.ValidationErrors = transaction.ValidationErrors;
                    var badResponse = Request.CreateResponse<RegisterInfo>(HttpStatusCode.BadRequest, objRegisterInfo);
                    return badResponse;
                }

                UserBusinessService userBusinessService = new UserBusinessService(userDataService);

                user objUser = userBusinessService.AddUser(
                    objclient.ID,
                    objRegisterInfo.UserName,
                    objRegisterInfo.FirstName,
                    objRegisterInfo.LastName,
                    objRegisterInfo.Password,
                    objRegisterInfo.PasswordConfirmation,
                    objRegisterInfo.Email,
                    objRegisterInfo.DefaultPage,
                    objRegisterInfo.Role,
                    out transaction
                    );

                if (transaction.ReturnStatus == false)
                {
                    objRegisterInfo.ReturnMessage = transaction.ReturnMessage;
                    objRegisterInfo.ReturnStatus = transaction.ReturnStatus;
                    objRegisterInfo.ValidationErrors = transaction.ValidationErrors;
                    var badResponse = Request.CreateResponse<RegisterInfo>(HttpStatusCode.BadRequest, objRegisterInfo);
                    return badResponse;
                }

                ApplicationInitializationBusinessService initializationBusinessService;
                initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
                List<applicationmenu> menuItems = initializationBusinessService.GetMenuItems(true, out transaction);

                if (transaction.ReturnStatus == false)
                {
                    objRegisterInfo.ReturnMessage = transaction.ReturnMessage;
                    objRegisterInfo.ReturnStatus = transaction.ReturnStatus;
                    var badResponse = Request.CreateResponse<RegisterInfo>(HttpStatusCode.BadRequest, objRegisterInfo);
                    return badResponse;
                }

                WebUtils.AddActivityLog(objUser.ID, objclient.ID, Constants.WATERCONS_MODULE_ADMIN, "Register.html","#Admin/Register", "WaterCons "+ objRegisterInfo.SubscriptionType+" Subscription Successful.");

                objRegisterInfo.IsAuthenicated = true;
                objRegisterInfo.ReturnStatus = transaction.ReturnStatus;
                objRegisterInfo.ReturnMessage.Add(RegisterInfo.CONTACT_USER_ADD);
                objRegisterInfo.MenuItems = menuItems;
                objRegisterInfo.User = objUser;


                FormsAuthentication.SetAuthCookie(objUser.ID.ToString(), createPersistentCookie: false);
                var response = Request.CreateResponse<RegisterInfo>(HttpStatusCode.OK, objRegisterInfo);
                return response;

            }
            else
            {
                objRegisterInfo.ReturnStatus = adminRules.ValidationStatus;
                objRegisterInfo.ReturnMessage = adminRules.ValidationMessage;
                objRegisterInfo.ValidationErrors = adminRules.ValidationErrors;
                
                var badResponse = Request.CreateResponse<RegisterInfo>(HttpStatusCode.BadRequest, objRegisterInfo);
                return badResponse;
            }
        }


        [Route("Login")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody] UserInfo objUserInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AdminBusinessService adminBusinessService;

            if (objUserInfo.UserName == null) objUserInfo.UserName = "";
            if (objUserInfo.Password == null) objUserInfo.Password = "";

            adminBusinessService = new AdminBusinessService(adminDataService);
            user objUser = adminBusinessService.Login(
                objUserInfo.UserName,
                objUserInfo.Password,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objUserInfo.ReturnMessage = transaction.ReturnMessage;
                objUserInfo.ReturnStatus = transaction.ReturnStatus;
                objUserInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<UserInfo>(HttpStatusCode.BadRequest, objUserInfo);
                return badResponse;
            }

            ApplicationInitializationBusinessService initializationBusinessService;
            initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
            List<applicationmenu> menuItems = initializationBusinessService.GetMenuItems(true, out transaction);

            if (transaction.ReturnStatus == false)
            {
                objUserInfo.ReturnMessage = transaction.ReturnMessage;
                objUserInfo.ReturnStatus = transaction.ReturnStatus;
                var badResponse = Request.CreateResponse<UserInfo>(HttpStatusCode.BadRequest, objUserInfo);
                return badResponse;
            }

            objUserInfo.ReturnStatus = transaction.ReturnStatus;
            objUserInfo.IsAuthenicated = true;
            objUserInfo.ReturnMessage.Add(UserInfo.LOGIN_SUCCESSFUL);
            objUserInfo.MenuItems = menuItems;
            objUserInfo.User = objUser;

            FormsAuthentication.SetAuthCookie(objUser.ID.ToString(), createPersistentCookie: false);

            var response = Request.CreateResponse<UserInfo>(HttpStatusCode.OK, objUserInfo);
            return response;
        }

        
        /*
        [Route("UpdateUser")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateUser(HttpRequestMessage request, [FromBody] UserInfo updateUserDTO)
        {

          //Guid userID = new Guid(User.Identity.Name);
          int userID = 1;

          AdminInfo accountsWebApiModel = new AdminInfo();
          TransactionalInformation transaction = new TransactionalInformation();
          AdminBusinessService accountsBusinessService;
          accountsWebApiModel.IsAuthenicated = true;

          if (updateUserDTO.FirstName == null) updateUserDTO.FirstName = "";
          if (updateUserDTO.LastName == null) updateUserDTO.LastName = "";
          if (updateUserDTO.EmailAddress == null) updateUserDTO.EmailAddress = "";
          if (updateUserDTO.UserName == null) updateUserDTO.UserName = "";
          if (updateUserDTO.Password == null) updateUserDTO.Password = "";
          if (updateUserDTO.PasswordConfirmation == null) updateUserDTO.PasswordConfirmation = "";

          accountsBusinessService = new AdminBusinessService(accountsDataService);
          user user = accountsBusinessService.UpdateUser(
              userID,
              updateUserDTO.FirstName,
              updateUserDTO.LastName,
              updateUserDTO.UserName,
              updateUserDTO.EmailAddress,
              updateUserDTO.Password,
              updateUserDTO.PasswordConfirmation,
              out transaction);

          if (transaction.ReturnStatus == false)
          {
            accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
            accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
            accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
            var badResponse = Request.CreateResponse<AdminInfo>(HttpStatusCode.BadRequest, accountsWebApiModel);
            return badResponse;
          }

          accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
          accountsWebApiModel.ReturnMessage.Add("User successful updated.");

          var response = Request.CreateResponse<AdminInfo>(HttpStatusCode.OK, accountsWebApiModel);
          return response;

        }


       

        [Route("GetUser")]
        [HttpGet]
        [ValidateModelState]
        public HttpResponseMessage GetUser()
        {

          //Guid userID = new Guid(User.Identity.Name);
          int userID = 1;

          AdminInfo accountsWebApiModel = new AdminInfo();
          TransactionalInformation transaction = new TransactionalInformation();
          AdminBusinessService accountsBusinessService;
          accountsWebApiModel.IsAuthenicated = true;

          accountsBusinessService = new AdminBusinessService(accountsDataService);
          user user = accountsBusinessService.GetUser(userID, out transaction);

          transaction.ReturnStatus = true;

          if (transaction.ReturnStatus == false)
          {
            accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
            accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
            accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
            var badResponse = Request.CreateResponse<AdminInfo>(HttpStatusCode.BadRequest, accountsWebApiModel);
            return badResponse;
          }

          accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
          accountsWebApiModel.User = user;

          var response = Request.CreateResponse<AdminInfo>(HttpStatusCode.OK, accountsWebApiModel);
          return response;
        }
        */
    }
}