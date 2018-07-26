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
    public class MasterTradesBusinessService
    {
        IMasterTradesDataService _masterTradessDataService;

        private IMasterTradesDataService masterTradesDataService
        {
            get { return _masterTradessDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MasterTradesBusinessService(IMasterTradesDataService dataService)
        {
            _masterTradessDataService = dataService;
        }


        public tmastertrade CreateMasterTrade(int MASTERID, int ACCOUNTID, string EXCHANGE, string TRADETYPE, string TRADINGPAIR, string BUYPRICE,
                        string BUYQUANTITY, decimal BUYTOTAL, string SELLPRICE, string SELLQUANTITY, string FIRSTACTION, DateTime LASTRUN, DateTime NEXTRUN,
                        int RUNFREQUENCY, int RUNLIMIT, int RUNCOUNT, string ACTIVE, string SELLTOTAL,
                        out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();
            MasterTradesBusinessRules masterTradesBusinessRules = new MasterTradesBusinessRules();
            tmastertrade masterTrade = new tmastertrade();

            masterTrade.MASTERID = MASTERID;
            masterTrade.ACCOUNTID = ACCOUNTID;
            masterTrade.EXCHANGE = EXCHANGE;
            masterTrade.TRADETYPE = TRADETYPE;
            masterTrade.TRADINGPAIR = TRADINGPAIR;
            masterTrade.BUYPRICE = BUYPRICE;
            masterTrade.BUYQUANTITY = BUYQUANTITY;
            masterTrade.BUYTOTAL = BUYTOTAL;
            masterTrade.SELLPRICE = SELLPRICE;
            masterTrade.SELLQUANTITY = SELLQUANTITY;
            masterTrade.FIRSTACTION = FIRSTACTION;
            masterTrade.LASTRUN = LASTRUN;
            masterTrade.NEXTRUN = NEXTRUN;
            masterTrade.RUNFREQUENCY = RUNFREQUENCY;
            masterTrade.RUNLIMIT = RUNLIMIT;
            masterTrade.RUNCOUNT = RUNCOUNT;
            masterTrade.ACTIVE = ACTIVE;
            masterTrade.SELLTOTAL = SELLTOTAL;

            try
            {

                masterTradesDataService.CreateSession();
               // masterTradesBusinessRules.ValidateMasterTrade(masterTrade, masterTradesDataService);

                //if (masterTradesBusinessRules.ValidationStatus == true)
                //{
                //    masterTradesDataService.BeginTransaction();
                //    masterTradesDataService.CreateMasterTrade(masterTrade);
                //    masterTradesDataService.CommitTransaction(true);
                //    transaction.ReturnStatus = true;
                //    transaction.ReturnMessage.Add("MasterTrade successfully created.");
                //}
                //else
                //{
                //    transaction.ReturnStatus = masterTradesBusinessRules.ValidationStatus;
                //    transaction.ReturnMessage = masterTradesBusinessRules.ValidationMessage;
                //    transaction.ValidationErrors = masterTradesBusinessRules.ValidationErrors;
                //}

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
                masterTradesDataService.CloseSession();
            }

            return masterTrade;
        }


        public tmastertrade UpdateMasterTrade(int MASTERID, int ACCOUNTID, string EXCHANGE, string TRADETYPE, string TRADINGPAIR, string BUYPRICE,
                        string BUYQUANTITY, decimal BUYTOTAL, string SELLPRICE, string SELLQUANTITY, string FIRSTACTION, DateTime LASTRUN, DateTime NEXTRUN,
                        int RUNFREQUENCY, int RUNLIMIT, int RUNCOUNT, string ACTIVE, string SELLTOTAL,
                                out TransactionalInformation transaction)

        {

            transaction = new TransactionalInformation();
            MasterTradesBusinessRules masterTradesBusinessRules = new MasterTradesBusinessRules();
            tmastertrade masterTrade = new tmastertrade();

            try
            {
                masterTradesDataService.CreateSession();
                
                masterTrade = masterTradesDataService.GetMasterTrade(MASTERID); 
                masterTrade.ACCOUNTID = ACCOUNTID;
                masterTrade.EXCHANGE = EXCHANGE;
                masterTrade.TRADETYPE = TRADETYPE;
                masterTrade.TRADINGPAIR = TRADINGPAIR;
                masterTrade.BUYPRICE = BUYPRICE;
                masterTrade.BUYQUANTITY = BUYQUANTITY;
                masterTrade.BUYTOTAL = BUYTOTAL;
                masterTrade.SELLPRICE = SELLPRICE;
                masterTrade.SELLQUANTITY = SELLQUANTITY;
                masterTrade.FIRSTACTION = FIRSTACTION;
                masterTrade.LASTRUN = LASTRUN;
                masterTrade.NEXTRUN = NEXTRUN;
                masterTrade.RUNFREQUENCY = RUNFREQUENCY;
                masterTrade.RUNLIMIT = RUNLIMIT;
                masterTrade.RUNCOUNT = RUNCOUNT;
                masterTrade.ACTIVE = ACTIVE;
                masterTrade.SELLTOTAL = SELLTOTAL;

                masterTradesBusinessRules.ValidateMasterTrade( masterTrade, masterTradesDataService);

                if (masterTradesBusinessRules.ValidationStatus == true)
                {
                    masterTradesDataService.BeginTransaction();
                    masterTradesDataService.UpdateMasterTrade(masterTrade);
                    masterTradesDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("MasterTrade successfully updated at " + DateTime.Now.ToShortDateString());
                }
                else
                {
                    transaction.ReturnStatus = masterTradesBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = masterTradesBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = masterTradesBusinessRules.ValidationErrors;
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
                masterTradesDataService.CloseSession();
            }

            return masterTrade;
        }


        public tmastertrade GetMasterTrade(int MASTERID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            tmastertrade masterTrade = new tmastertrade();

            try
            {
                masterTradesDataService.CreateSession();
                masterTrade = masterTradesDataService.GetMasterTrade(MASTERID);
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
                masterTradesDataService.CloseSession();
            }
            return masterTrade;
        }


        public List<tmastertrade> MasterTradeInquiry(string TradeType, string TradingPair, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<tmastertrade> masterTradeList = new List<tmastertrade>();

            try
            {
                masterTradesDataService.CreateSession();
                masterTradeList = masterTradesDataService.MasterTradeInquiry(TradeType, TradingPair, paging, out transaction);
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
                masterTradesDataService.CloseSession();
            }

            return masterTradeList;
        }

    }
}
