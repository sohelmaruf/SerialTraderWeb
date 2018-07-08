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
    [RoutePrefix("api/masterTrades")]
    public class MasterTradeApiController : ApiController
    {

        IMasterTradesDataService masterTradesDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public MasterTradeApiController()
        {
            masterTradesDataService = new MasterTradesDataService();
        }

        [Route("InitializeMasterTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage InitializeMasterTrade(HttpRequestMessage request, [FromBody] MasterTradeInfo objMasterTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            objMasterTradeInfo.IsAuthenicated = true;

            if (transaction.ReturnStatus == false)
            {
                objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objMasterTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.BadRequest, objMasterTradeInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.OK, objMasterTradeInfo);
            return response;
        }

        [Route("GetMasterTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage GetMasterTrade(HttpRequestMessage request, [FromBody] MasterTradeInfo objMasterTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            MasterTradesBusinessService masterTradesBusinessService;
            objMasterTradeInfo.IsAuthenicated = true;

            masterTradesBusinessService = new MasterTradesBusinessService(masterTradesDataService);
            tmastertrade masterTrade = masterTradesBusinessService.GetMasterTrade(objMasterTradeInfo.MASTERID, out transaction);

            if (transaction.ReturnStatus == false)
            {
                objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objMasterTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.BadRequest, objMasterTradeInfo);
                return badResponse;
            }

            var response = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.OK, objMasterTradeInfo);
            return response;
        }


        [Route("GetMasterTrades")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetMasterTrades([FromBody] MasterTradeInfo objMasterTradeInfo)
        {
            if (objMasterTradeInfo.TRADETYPE == null) objMasterTradeInfo.TRADETYPE= string.Empty;
            if (objMasterTradeInfo.TRADINGPAIR == null) objMasterTradeInfo.TRADINGPAIR = string.Empty;
            if (objMasterTradeInfo.SortDirection == null) objMasterTradeInfo.SortDirection = string.Empty;
            if (objMasterTradeInfo.SortExpression == null) objMasterTradeInfo.SortExpression = string.Empty;

            TransactionalInformation transaction = new TransactionalInformation();
            MasterTradesBusinessService masterTradesBusinessService;

            objMasterTradeInfo.IsAuthenicated = true;

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = objMasterTradeInfo.CurrentPageNumber;
            paging.PageSize = objMasterTradeInfo.PageSize;
            paging.SortExpression = objMasterTradeInfo.SortExpression;
            paging.SortDirection = objMasterTradeInfo.SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "DESC";
            if (paging.SortExpression == "") paging.SortExpression = "FirstName";

            masterTradesBusinessService = new MasterTradesBusinessService(masterTradesDataService);

            List<tmastertrade> masterTrades = masterTradesBusinessService.MasterTradeInquiry(objMasterTradeInfo.TRADETYPE, objMasterTradeInfo.TRADINGPAIR, paging, out transaction);

            objMasterTradeInfo.MasterTrades = masterTrades;
            objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objMasterTradeInfo.TotalPages = transaction.TotalPages;
            objMasterTradeInfo.TotalRows = transaction.TotalRows;
            objMasterTradeInfo.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.OK, objMasterTradeInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.BadRequest, objMasterTradeInfo);
            return badResponse;
        }

        [Route("CreateMasterTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateMasterTrade(HttpRequestMessage request, [FromBody] MasterTradeInfo objMasterTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            MasterTradesBusinessService masterTradesBusinessService;
            objMasterTradeInfo.IsAuthenicated = true;
            masterTradesBusinessService = new MasterTradesBusinessService(masterTradesDataService);

            tmastertrade masterTrade = masterTradesBusinessService.CreateMasterTrade(
                    objMasterTradeInfo.MASTERID,
                    objMasterTradeInfo.ACCOUNTID,
                    objMasterTradeInfo.EXCHANGE,
                    objMasterTradeInfo.TRADETYPE,
                    objMasterTradeInfo.TRADINGPAIR,
                    objMasterTradeInfo.BUYPRICE,
                    objMasterTradeInfo.BUYQUANTITY,
                    objMasterTradeInfo.BUYTOTAL,
                    objMasterTradeInfo.SELLPRICE,
                    objMasterTradeInfo.SELLQUANTITY,
                    objMasterTradeInfo.FIRSTACTION,
                    objMasterTradeInfo.LASTRUN,
                    objMasterTradeInfo.NEXTRUN,
                    objMasterTradeInfo.RUNFREQUENCY,
                    objMasterTradeInfo.RUNLIMIT,
                    objMasterTradeInfo.RUNCOUNT,
                    objMasterTradeInfo.ACTIVE,
                    objMasterTradeInfo.SELLTOTAL,
                    out transaction);

            if (transaction.ReturnStatus == false)
            {
                objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objMasterTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.BadRequest, objMasterTradeInfo);
                return badResponse;
            }

            objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objMasterTradeInfo.MasterTrade = masterTrade;

            var response = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.OK, objMasterTradeInfo);
            return response;
        }

        [Route("UpdateMasterTrade")]
        [WebApiAuthenication]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateMasterTrade(HttpRequestMessage request, [FromBody] MasterTradeInfo objMasterTradeInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            MasterTradesBusinessService masterTradesBusinessService;
            objMasterTradeInfo.IsAuthenicated = true;

            masterTradesBusinessService = new MasterTradesBusinessService(masterTradesDataService);

            tmastertrade masterTrade = masterTradesBusinessService.UpdateMasterTrade(
                    objMasterTradeInfo.MASTERID,
                    objMasterTradeInfo.ACCOUNTID,
                    objMasterTradeInfo.EXCHANGE,
                    objMasterTradeInfo.TRADETYPE,
                    objMasterTradeInfo.TRADINGPAIR,
                    objMasterTradeInfo.BUYPRICE,
                    objMasterTradeInfo.BUYQUANTITY,
                    objMasterTradeInfo.BUYTOTAL,
                    objMasterTradeInfo.SELLPRICE,
                    objMasterTradeInfo.SELLQUANTITY,
                    objMasterTradeInfo.FIRSTACTION,
                    objMasterTradeInfo.LASTRUN,
                    objMasterTradeInfo.NEXTRUN,
                    objMasterTradeInfo.RUNFREQUENCY,
                    objMasterTradeInfo.RUNLIMIT,
                    objMasterTradeInfo.RUNCOUNT,
                    objMasterTradeInfo.ACTIVE,
                    objMasterTradeInfo.SELLTOTAL,
                    out transaction);

            if (transaction.ReturnStatus == false)
            {
                objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
                objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
                objMasterTradeInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.BadRequest, objMasterTradeInfo);
                return badResponse;
            }

            objMasterTradeInfo.ReturnStatus = transaction.ReturnStatus;
            objMasterTradeInfo.ReturnMessage = transaction.ReturnMessage;
            objMasterTradeInfo.MasterTrade = masterTrade;

            var response = Request.CreateResponse<MasterTradeInfo>(HttpStatusCode.OK, objMasterTradeInfo);
            return response;

        }

    }
}