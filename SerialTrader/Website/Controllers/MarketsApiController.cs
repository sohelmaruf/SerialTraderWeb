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
    [RoutePrefix("api/markets")]
    public class MarketsApiController : ApiController
    {
        IMarketDataService marketDataService;
    
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public MarketsApiController()
        {
            marketDataService = new MarketDataService();
        }
        
        [Route("InitializeMarket")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeMarket(HttpRequestMessage request, [FromBody] MarketInfo objMarketInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            objMarketInfo.IsAuthenicated = true;

            if (transaction.ReturnStatus == false)
            {
                objMarketInfo.ReturnMessage = transaction.ReturnMessage;
                objMarketInfo.ReturnStatus = transaction.ReturnStatus;
                objMarketInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<MarketInfo>(HttpStatusCode.BadRequest, objMarketInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<MarketInfo>(HttpStatusCode.OK, objMarketInfo);
            return response;
        }

        [Route("GetMarket")]
        [HttpGet]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetMarket(int MarketID)
        {
            MarketInfo objMarketInfo = new MarketInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            MarketBusinessService marketBusinessService;

            objMarketInfo.IsAuthenicated = true;

            marketBusinessService = new MarketBusinessService(marketDataService);

            tmarket market = marketBusinessService.GetMarket(MarketID, out transaction);

            objMarketInfo.Market = market;
            objMarketInfo.IsAuthenicated = true;
            objMarketInfo.ReturnStatus = transaction.ReturnStatus;
            objMarketInfo.ReturnMessage = transaction.ReturnMessage;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<MarketInfo>(HttpStatusCode.OK, objMarketInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<MarketInfo>(HttpStatusCode.BadRequest, objMarketInfo);
            return badResponse;
        }

        
        [Route("GetMarkets")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetMarkets()
        {
            MarketInfo objMarketInfo = new MarketInfo();
            TransactionalInformation transaction = new TransactionalInformation();
            MarketBusinessService marketBusinessService;

            objMarketInfo.IsAuthenicated = true;

            marketBusinessService = new MarketBusinessService(marketDataService);

            
            List<tmarket> markets = marketBusinessService.GetMarkets(out transaction);

            objMarketInfo.Markets = markets;
            objMarketInfo.ReturnStatus = transaction.ReturnStatus;
            objMarketInfo.ReturnMessage = transaction.ReturnMessage;
            
            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<MarketInfo>(HttpStatusCode.OK, objMarketInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<MarketInfo>(HttpStatusCode.BadRequest, objMarketInfo);
            return badResponse;
        }
    }
}