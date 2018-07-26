using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

//using Website.Helpers;
using Website.Filters;

using AppLibrary.Interfaces;
using AppLibrary.DataServices;
using AppLibrary.Business;
using AppLibrary.Entity;
using AppLibrary.Common;
using AppLibrary.Model;

namespace Website.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsApiController : ApiController
    {
     
        IAccountsDataService accountsDataService;
    
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public AccountsApiController()
        {
            accountsDataService = new AccountsDataService();
        }
        
        [Route("InitializeAccount")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeAccount(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            objAccountInfo.IsAuthenicated = true;

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;
        }

        [Route("GetAccount")]
        [HttpGet]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetAccount(int accountID)
        {
            AccountInfo objAccountInfo = new AccountInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;

            objAccountInfo.IsAuthenicated = true;

            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            taccount account = accountsBusinessService.GetAccount(accountID, out transaction);

            objAccountInfo.Account = account;
            objAccountInfo.IsAuthenicated = true;
            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
            return badResponse;
        }

        
        [Route("GetAccounts")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetAccounts([FromBody] AccountInfo objAccountInfo)
        {
            if (objAccountInfo.FirstName == null) objAccountInfo.FirstName = string.Empty;
            if (objAccountInfo.LastName == null) objAccountInfo.LastName = string.Empty;
            if (objAccountInfo.SortDirection == null) objAccountInfo.SortDirection = string.Empty;
            if (objAccountInfo.SortExpression == null) objAccountInfo.SortExpression = string.Empty;
            
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;

            objAccountInfo.IsAuthenicated = true;

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = objAccountInfo.CurrentPageNumber;
            paging.PageSize = objAccountInfo.PageSize;
            paging.SortExpression = objAccountInfo.SortExpression;
            paging.SortDirection = objAccountInfo.SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "DESC";
            if (paging.SortExpression == "") paging.SortExpression = "FirstName";

            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            List<taccount> accounts = accountsBusinessService.AccountInquiry(objAccountInfo.FirstName, objAccountInfo.LastName, paging, out transaction);
            
            objAccountInfo.Accounts = accounts;
            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.TotalPages = transaction.TotalPages;
            objAccountInfo.TotalRows = transaction.TotalRows;
            objAccountInfo.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
            return badResponse;
        }
        
        [Route("CreateAccount")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateAccount(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;
            objAccountInfo.IsAuthenicated = true;
            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            taccount account = accountsBusinessService.CreateAccount(
                objAccountInfo.AccountID,
                objAccountInfo.FirstName,
                objAccountInfo.LastName,
                objAccountInfo.EmailAddress,
                objAccountInfo.IsEnabled,
                objAccountInfo.PostToSlack,
                objAccountInfo.SlackBotChannel, 
                objAccountInfo.Password, 
                objAccountInfo.Role,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.Account= account;

            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;
        }

        [Route("UpdateAccount")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateAccount(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;
            objAccountInfo.IsAuthenicated = true;
            
            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            taccount account = accountsBusinessService.UpdateAccount(
                objAccountInfo.AccountID,
                objAccountInfo.FirstName,
                objAccountInfo.LastName,
                objAccountInfo.EmailAddress,
                objAccountInfo.IsEnabled,
                objAccountInfo.PostToSlack,
                objAccountInfo.SlackBotChannel,
                objAccountInfo.Password,
                objAccountInfo.Role,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.Account = account;

            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;

        }
        
        [Route("CreateAccountLineItem")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateAccountLineItem(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;
            objAccountInfo.IsAuthenicated = true;

            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            taccount account= accountsBusinessService.CreateAccountDetailLineItem(
                objAccountInfo.AccountID,
                objAccountInfo.AccountID,        
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }


            List<taccount> accounts = accountsBusinessService.GetAccountDetails(objAccountInfo.AccountID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }


            taccount acc = accountsBusinessService.GetAccount(objAccountInfo.AccountID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            transaction.ReturnMessage.Add("Detail line item succcessfully added.");

            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.Accounts = accounts;
            objAccountInfo.Account = acc;

            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;
        }
        
        [Route("UpdateAccountLineItem")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateAccountLineItem(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;
            objAccountInfo.IsAuthenicated = true;

            accountsBusinessService = new AccountsBusinessService(accountsDataService);

            taccount accountDetail= accountsBusinessService.UpdateAccountDetailLineItem(
                objAccountInfo.AccountID,
                objAccountInfo.AccountID,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            taccount account = accountsBusinessService.GetAccount(objAccountInfo.AccountID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            transaction.ReturnMessage.Add("Detail line item successfully updated.");

            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.Account = account;

            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;
        }

        
        [Route("DeleteAccountLineItem")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage DeleteAccountLineItem(HttpRequestMessage request, [FromBody] AccountInfo objAccountInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            AccountsBusinessService accountsBusinessService;
            objAccountInfo.IsAuthenicated = true;

            accountsBusinessService = new AccountsBusinessService(accountsDataService);
            accountsBusinessService.DeleteAccountDetailLineItem(
                objAccountInfo.AccountID,               
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            taccount account = accountsBusinessService.GetAccount(objAccountInfo.AccountID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                objAccountInfo.ReturnMessage = transaction.ReturnMessage;
                objAccountInfo.ReturnStatus = transaction.ReturnStatus;
                objAccountInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<AccountInfo>(HttpStatusCode.BadRequest, objAccountInfo);
                return badResponse;
            }

            objAccountInfo.ReturnStatus = transaction.ReturnStatus;
            objAccountInfo.ReturnMessage = transaction.ReturnMessage;
            objAccountInfo.Account = account;
            
            var response = Request.CreateResponse<AccountInfo>(HttpStatusCode.OK, objAccountInfo);
            return response;
        }


    }
}