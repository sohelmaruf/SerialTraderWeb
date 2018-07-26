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
    [RoutePrefix("api/exchanges")]
    public class ExchangesApiController : ApiController
    {
        IExchangeDataService exchangeDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public ExchangesApiController()
        {
            exchangeDataService = new ExchangeDataService();
        }

        [Route("InitializeExchange")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeExchange(HttpRequestMessage request, [FromBody] ExchangeInfo objExchangeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            objExchangeInfo.IsAuthenicated = true;

            if (transaction.ReturnStatus == false)
            {
                objExchangeInfo.ReturnMessage = transaction.ReturnMessage;
                objExchangeInfo.ReturnStatus = transaction.ReturnStatus;
                objExchangeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.BadRequest, objExchangeInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.OK, objExchangeInfo);
            return response;
        }

        [Route("GetExchange")]
        [HttpGet]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetExchange(int ExchangeID)
        {
            ExchangeInfo objExchangeInfo = new ExchangeInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            ExchangeBusinessService exchangeBusinessService;

            objExchangeInfo.IsAuthenicated = true;

            exchangeBusinessService = new ExchangeBusinessService(exchangeDataService);
            texchanx exchange = exchangeBusinessService.GetExchange(ExchangeID, out transaction);

            objExchangeInfo.Exchange = exchange;
            objExchangeInfo.IsAuthenicated = true;
            objExchangeInfo.ReturnStatus = transaction.ReturnStatus;
            objExchangeInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.OK, objExchangeInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.BadRequest, objExchangeInfo);
            return badResponse;
        }


        [Route("GetExchanges")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetExchanges()
        {
            ExchangeInfo objExchangeInfo = new ExchangeInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            ExchangeBusinessService exchangeBusinessService;

            objExchangeInfo.IsAuthenicated = true;

            exchangeBusinessService = new ExchangeBusinessService(exchangeDataService);
            List<texchanx> exchanges = exchangeBusinessService.GetExchanges(out transaction);

            objExchangeInfo.Exchanges = exchanges;
            objExchangeInfo.ReturnStatus = transaction.ReturnStatus;
            objExchangeInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.OK, objExchangeInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<ExchangeInfo>(HttpStatusCode.BadRequest, objExchangeInfo);
            return badResponse;
        }

    }
}