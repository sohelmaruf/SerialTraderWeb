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
    [RoutePrefix("api/trades")]
    public class TradesApiController : ApiController
    {
     
        ITradesDataService tradesDataService;
    
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public TradesApiController()
        {
            tradesDataService = new TradesDataService();
        }
        
        [Route("InitializeTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeTrade(HttpRequestMessage request, [FromBody] TradeInfo objTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            objTradeInfo.IsAuthenicated = true;

            if (transaction.ReturnStatus == false)
            {
                objTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<TradeInfo>(HttpStatusCode.BadRequest, objTradeInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<TradeInfo>(HttpStatusCode.OK, objTradeInfo);
            return response;
        }
        
        [Route("GetTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage GetTrade(HttpRequestMessage request, [FromBody] TradeInfo objTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            TradesBusinessService tradesBusinessService;
            objTradeInfo.IsAuthenicated = true;

            tradesBusinessService = new TradesBusinessService(tradesDataService);
            ttrade trade = tradesBusinessService.GetTrade(objTradeInfo.TID, out transaction);

            if (transaction.ReturnStatus == false)
            {
                objTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<TradeInfo>(HttpStatusCode.BadRequest, objTradeInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<TradeInfo>(HttpStatusCode.OK, objTradeInfo);
            return response;            
        }


        [Route("GetTrades")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetTrades([FromBody] TradeInfo objTradeInfo)
        {
            if (objTradeInfo.ORDERID == null) objTradeInfo.ORDERID = string.Empty;
            if (objTradeInfo.ORDERSTATUS == null) objTradeInfo.ORDERSTATUS= string.Empty;
            if (objTradeInfo.SortDirection == null) objTradeInfo.SortDirection = string.Empty;
            if (objTradeInfo.SortExpression == null) objTradeInfo.SortExpression = string.Empty;
            
            TransactionalInformation transaction = new TransactionalInformation();
            TradesBusinessService tradesBusinessService;

            objTradeInfo.IsAuthenicated = true;

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = objTradeInfo.CurrentPageNumber;
            paging.PageSize = objTradeInfo.PageSize;
            paging.SortExpression = objTradeInfo.SortExpression;
            paging.SortDirection = objTradeInfo.SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "DESC";
            if (paging.SortExpression == "") paging.SortExpression = "FirstName";

            tradesBusinessService = new TradesBusinessService(tradesDataService);

            List<ttrade> trades = tradesBusinessService.TradeInquiry(objTradeInfo.ORDERID, objTradeInfo.ORDERSTATUS, paging, out transaction);

            objTradeInfo.Trades = trades;
            objTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objTradeInfo.TotalPages = transaction.TotalPages;
            objTradeInfo.TotalRows = transaction.TotalRows;
            objTradeInfo.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<TradeInfo>(HttpStatusCode.OK, objTradeInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<TradeInfo>(HttpStatusCode.BadRequest, objTradeInfo);
            return badResponse;
        }
        
        [Route("CreateTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateTrade(HttpRequestMessage request, [FromBody] TradeInfo objTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            TradesBusinessService tradesBusinessService;
            objTradeInfo.IsAuthenicated = true;
            tradesBusinessService = new TradesBusinessService(tradesDataService);

            ttrade trade = tradesBusinessService.CreateTrade(
                objTradeInfo.TID,
                objTradeInfo.ACCOUNTID,
                objTradeInfo.EXCHANGE,
                objTradeInfo.MASTERID,
                objTradeInfo.TRADEGROUPID,
                objTradeInfo.TRADINGACTION,
                objTradeInfo.TRADINGPAIR,
                objTradeInfo.TRADINGPRICE,
                objTradeInfo.ACTUALRATE,
                objTradeInfo.TRADINGQTY,
                objTradeInfo.TOTAL,
                objTradeInfo.ORDERID,
                objTradeInfo.ORDERSTATUS,
                objTradeInfo.DATETIME,
                objTradeInfo.ORDERRESULT,
                objTradeInfo.RETRYIFCANCELED,
                objTradeInfo.USEEXCHANGEPRICE,
                objTradeInfo.ERRORRESULT,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<TradeInfo>(HttpStatusCode.BadRequest, objTradeInfo);
                return badResponse;
            }

            objTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objTradeInfo.Trade= trade;

            var response = Request.CreateResponse<TradeInfo>(HttpStatusCode.OK, objTradeInfo);
            return response;
        }

        [Route("UpdateTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateTrade(HttpRequestMessage request, [FromBody] TradeInfo objTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            TradesBusinessService tradesBusinessService;
            objTradeInfo.IsAuthenicated = true;

            tradesBusinessService = new TradesBusinessService(tradesDataService);

            ttrade trade = tradesBusinessService.UpdateTrade(
                 objTradeInfo.TID,
                objTradeInfo.ACCOUNTID,
                objTradeInfo.EXCHANGE,
                objTradeInfo.MASTERID,
                objTradeInfo.TRADEGROUPID,
                objTradeInfo.TRADINGACTION,
                objTradeInfo.TRADINGPAIR,
                objTradeInfo.TRADINGPRICE,
                objTradeInfo.ACTUALRATE,
                objTradeInfo.TRADINGQTY,
                objTradeInfo.TOTAL,
                objTradeInfo.ORDERID,
                objTradeInfo.ORDERSTATUS,
                objTradeInfo.DATETIME,
                objTradeInfo.ORDERRESULT,
                objTradeInfo.RETRYIFCANCELED,
                objTradeInfo.USEEXCHANGEPRICE,
                objTradeInfo.ERRORRESULT,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<TradeInfo>(HttpStatusCode.BadRequest, objTradeInfo);
                return badResponse;
            }

            objTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objTradeInfo.Trade = trade;

            var response = Request.CreateResponse<TradeInfo>(HttpStatusCode.OK, objTradeInfo);
            return response;

        }

    }
}