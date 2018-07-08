using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;

namespace AppLibrary.Business
{
    public class TradesBusinessService
    {
        ITradesDataService _tradessDataService;

        private ITradesDataService tradesDataService
        {
            get { return _tradessDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TradesBusinessService(ITradesDataService dataService)
        {
            _tradessDataService = dataService;
        }


        public ttrade CreateTrade(int TID, int ACCOUNTID, string EXCHANGE, int MASTERID, string TRADEGROUPID, string TRADINGACTION,
                        string TRADINGPAIR, decimal TRADINGPRICE, decimal ACTUALRATE, decimal TRADINGQTY, decimal TOTAL, string ORDERID,
                        string ORDERSTATUS, DateTime DATETIME, string ORDERRESULT, string RETRYIFCANCELED, string USEEXCHANGEPRICE, string ERRORRESULT,
                        out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();
            TradesBusinessRules tradesBusinessRules = new TradesBusinessRules();
            ttrade trade = new ttrade();

            trade.TID = TID;
            trade.ACCOUNTID = ACCOUNTID;
            trade.EXCHANGE = EXCHANGE;
            trade.MASTERID = MASTERID;
            trade.TRADEGROUPID = TRADEGROUPID;
            trade.TRADINGACTION = TRADINGACTION;
            trade.TRADINGPAIR = TRADINGPAIR;
            trade.TRADINGPRICE = TRADINGPRICE;
            trade.ACTUALRATE = ACTUALRATE;
            trade.TRADINGQTY = TRADINGQTY;
            trade.TOTAL = TOTAL;
            trade.ORDERID = ORDERID;
            trade.ORDERSTATUS = ORDERSTATUS;
            trade.DATETIME = DATETIME;
            trade.ORDERRESULT = ORDERRESULT;
            trade.RETRYIFCANCELED = RETRYIFCANCELED;
            trade.USEEXCHANGEPRICE = USEEXCHANGEPRICE;
            trade.ERRORRESULT = ERRORRESULT;
            
            try
            {

                tradesDataService.CreateSession();
                tradesBusinessRules.ValidateTrade(trade, tradesDataService);

                if (tradesBusinessRules.ValidationStatus == true)
                {
                    tradesDataService.BeginTransaction();
                    tradesDataService.CreateTrade(trade);
                    tradesDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Trade successfully created.");
                }
                else
                {
                    transaction.ReturnStatus = tradesBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = tradesBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = tradesBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                tradesDataService.CloseSession();
            }

            return trade;
        }


        public ttrade UpdateTrade(int TID, int ACCOUNTID, string EXCHANGE, int MASTERID, string TRADEGROUPID, string TRADINGACTION,
                        string TRADINGPAIR, decimal TRADINGPRICE, decimal ACTUALRATE, decimal TRADINGQTY, decimal TOTAL, string ORDERID,
                        string ORDERSTATUS, DateTime DATETIME, string ORDERRESULT, string RETRYIFCANCELED, string USEEXCHANGEPRICE, string ERRORRESULT,
                                out TransactionalInformation transaction)

        {

            transaction = new TransactionalInformation();
            TradesBusinessRules tradesBusinessRules = new TradesBusinessRules();
            ttrade trade = new ttrade();

            try
            {
                tradesDataService.CreateSession();

                trade = tradesDataService.GetTrade(TID);
                trade.ACCOUNTID = ACCOUNTID;
                trade.EXCHANGE = EXCHANGE;
                trade.MASTERID = MASTERID;
                trade.TRADEGROUPID = TRADEGROUPID;
                trade.TRADINGACTION = TRADINGACTION;
                trade.TRADINGPAIR = TRADINGPAIR;
                trade.TRADINGPRICE = TRADINGPRICE;
                trade.ACTUALRATE = ACTUALRATE;
                trade.TRADINGQTY = TRADINGQTY;
                trade.TOTAL = TOTAL;
                trade.ORDERID = ORDERID;
                trade.ORDERSTATUS = ORDERSTATUS;
                trade.DATETIME = DATETIME;
                trade.ORDERRESULT = ORDERRESULT;
                trade.RETRYIFCANCELED = RETRYIFCANCELED;
                trade.USEEXCHANGEPRICE = USEEXCHANGEPRICE;
                trade.ERRORRESULT = ERRORRESULT;

                tradesBusinessRules.ValidateTrade(trade, tradesDataService);

                if (tradesBusinessRules.ValidationStatus == true)
                {
                    tradesDataService.BeginTransaction();
                    tradesDataService.UpdateTrade(trade);
                    tradesDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Trade successfully updated at " + DateTime.Now.ToShortDateString());
                }
                else
                {
                    transaction.ReturnStatus = tradesBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = tradesBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = tradesBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                tradesDataService.CloseSession();
            }

            return trade;
        }


        public ttrade GetTrade(int TID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            ttrade trade = new ttrade();

            try
            {
                tradesDataService.CreateSession();
                trade = tradesDataService.GetTrade(TID);
                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                tradesDataService.CloseSession();
            }
            return trade;
        }

     
        public List<ttrade> TradeInquiry(string OrderID, string OrderStatus, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ttrade> tradeList = new List<ttrade>();

            try
            {
                tradesDataService.CreateSession();
                tradeList = tradesDataService.TradeInquiry(OrderID, OrderStatus, paging, out transaction);
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                tradesDataService.CloseSession();
            }

            return tradeList;
        }

    }
}
