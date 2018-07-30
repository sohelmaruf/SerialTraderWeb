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

//using Website.Helpers;
using Website.Filters;

using AppLibrary.Interfaces;
using AppLibrary.DataServices;
using AppLibrary.Business;
using AppLibrary.Entity;
using AppLibrary.Model;
using AppLibrary.Common;

namespace Website.Controllers
{

    [RoutePrefix("api/keys")]
    public class APIKeysController : ApiController
    {
        private serialtraderEntities db = new serialtraderEntities();

        IAPIDataService apiDataService;
        IExchangeDataService exchangesDataService;

        public APIKeysController()
        {
            apiDataService = new APIDataService();
            exchangesDataService = new ExchangeDataService();
        }


        [Route("InitializeAPI")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeAPI(HttpRequestMessage request, [FromBody] APIInfo objAPIInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            APIBusinessService apiBusinessService = new APIBusinessService(apiDataService);
            ExchangeBusinessService exchangesBusinessService = new ExchangeBusinessService(exchangesDataService);

            objAPIInfo.IsAuthenicated = true;

            tkey key = apiBusinessService.GetAPI(objAPIInfo.KEYID, out transaction);

            List<texchanx> exchanges = exchangesBusinessService.GetExchanges(out transaction);

            objAPIInfo.Key = key;
            objAPIInfo.Exchanges = exchanges;
            objAPIInfo.ReturnStatus = transaction.ReturnStatus;
            objAPIInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<APIInfo>(HttpStatusCode.OK, objAPIInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
            return badResponse;
        }

        [Route("CreateAPI")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateAPI(HttpRequestMessage request, [FromBody] APIInfo objAPIInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            APIBusinessService apiBusinessService;

            if ((string.IsNullOrEmpty(objAPIInfo.EXCHANGE)) && (string.IsNullOrEmpty(objAPIInfo.APIKEY)))
            {
                objAPIInfo.ReturnStatus = false;
                objAPIInfo.ReturnMessage.Add("Please leave any of your API KEY information");
                var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
                return badResponse;
            }

            apiBusinessService = new APIBusinessService(apiDataService);
            tkey objKey = apiBusinessService.AddAPI(
                objAPIInfo.ACCOUNTID,
                objAPIInfo.EXCHANGE,
                objAPIInfo.APIKEY,
                objAPIInfo.APISECRET,
                objAPIInfo.PASSPHRASE,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAPIInfo.ReturnMessage = transaction.ReturnMessage;
                objAPIInfo.ReturnStatus = transaction.ReturnStatus;
                objAPIInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
                return badResponse;
            }

            objAPIInfo.IsAuthenicated = true;
            objAPIInfo.ReturnStatus = transaction.ReturnStatus;
            objAPIInfo.ReturnMessage.Add("Register API KEY successful.");

            var response = Request.CreateResponse<APIInfo>(HttpStatusCode.OK, objAPIInfo);
            return response;
        }


        [Route("UpdateAPI")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateAPI(HttpRequestMessage request, [FromBody] APIInfo objAPIInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            APIBusinessService apiBusinessService = new APIBusinessService(apiDataService);

            tkey objKey = apiBusinessService.UpdateAPI(
                objAPIInfo.KEYID,
                 objAPIInfo.ACCOUNTID,
                 objAPIInfo.EXCHANGE,
                 objAPIInfo.APIKEY,
                 objAPIInfo.APISECRET,
                 objAPIInfo.PASSPHRASE,
                 out transaction);

            if (transaction.ReturnStatus == false)
            {
                objAPIInfo.ReturnMessage = transaction.ReturnMessage;
                objAPIInfo.ReturnStatus = transaction.ReturnStatus;
                objAPIInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
                return badResponse;
            }

            objAPIInfo.ReturnStatus = transaction.ReturnStatus;
            objAPIInfo.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<APIInfo>(HttpStatusCode.OK, objAPIInfo);
            return response;
        }

        [Route("GetAPIs")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetAPIs([FromBody] APIInfo objAPIInfo)
        {
            if (objAPIInfo.EXCHANGE == null) objAPIInfo.EXCHANGE = string.Empty;
            if (objAPIInfo.APIKEY == null) objAPIInfo.APIKEY = string.Empty;
            if (objAPIInfo.SortDirection == null) objAPIInfo.SortDirection = string.Empty;
            if (objAPIInfo.SortExpression == null) objAPIInfo.SortExpression = string.Empty;

            TransactionalInformation transaction = new TransactionalInformation();
            APIBusinessService apiBusinessService = new APIBusinessService(apiDataService);

            objAPIInfo.IsAuthenicated = true;

            DataGridPagingInformation paging = new DataGridPagingInformation();
            //paging.CurrentPageNumber = objAPIInfo.CurrentPageNumber;
            paging.CurrentPageNumber = 1;
            //paging.PageSize = objAPIInfo.PageSize;
            paging.PageSize = 15;
            paging.SortExpression = objAPIInfo.SortExpression;
            paging.SortDirection = objAPIInfo.SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "DESC";
            if (paging.SortExpression == "") paging.SortExpression = "KEYID";
            
            List<tkey> keys = apiBusinessService.APIInquiry(objAPIInfo.EXCHANGE, objAPIInfo.APIKEY, paging, out transaction);

            objAPIInfo.Keys = keys;
            objAPIInfo.ReturnStatus = transaction.ReturnStatus;
            objAPIInfo.ReturnMessage = transaction.ReturnMessage;
            objAPIInfo.TotalPages = transaction.TotalPages;
            objAPIInfo.TotalRows = transaction.TotalRows;
            objAPIInfo.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<APIInfo>(HttpStatusCode.OK, objAPIInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
            return badResponse;
        }


        [Route("GetAPI")]
        [HttpGet]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetAPI(int keyID)
        {
            APIInfo objAPIInfo = new APIInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            APIBusinessService apiBusinessService = new APIBusinessService(apiDataService);

            objAPIInfo.IsAuthenicated = true;

            tkey key = apiBusinessService.GetAPI(keyID, out transaction);

            objAPIInfo.Key = key;
            objAPIInfo.IsAuthenicated = true;
            objAPIInfo.ReturnStatus = transaction.ReturnStatus;
            objAPIInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<APIInfo>(HttpStatusCode.OK, objAPIInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<APIInfo>(HttpStatusCode.BadRequest, objAPIInfo);
            return badResponse;
        }

    }
}