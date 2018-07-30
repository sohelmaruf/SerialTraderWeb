using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;

namespace AppLibrary.Business
{
    public class APIBusinessService
    {
        IAPIDataService _APIDataService;

        private IAPIDataService APIDataService
        {
            get { return _APIDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public APIBusinessService(IAPIDataService dataService)
        {
            _APIDataService = dataService;
        }

        public tkey AddAPI(int ACCOUNTID, string EXCHANGE, string APIKEY, string APISECRET, string PASSPHRASE, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            APIBusinessRules apiRules = new APIBusinessRules();

            tkey objtkey = new tkey();

            try
            {
                objtkey.ACCOUNTID = ACCOUNTID;
                objtkey.EXCHANGE = EXCHANGE;
                objtkey.APIKEY = APIKEY;
                objtkey.APISECRET = APISECRET;
                objtkey.PASSPHRASE = PASSPHRASE;
                
                APIDataService.CreateSession();
                apiRules.ValidateAPI(objtkey, APIDataService);

                if (apiRules.ValidationStatus == true)
                {
                    APIDataService.BeginTransaction();
                    APIDataService.AddAPI(objtkey);
                    APIDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("API KEY added successfully.");
                }
                else
                {
                    transaction.ReturnStatus = apiRules.ValidationStatus;
                    transaction.ReturnMessage = apiRules.ValidationMessage;
                    transaction.ValidationErrors = apiRules.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                APIDataService.CloseSession();
            }

            return objtkey;
        }


        public tkey UpdateAPI(int KEYID, int ACCOUNTID, string EXCHANGE, string APIKEY, string APISECRET, string PASSPHRASE, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            tkey objtkey = new tkey();

            try
            {
                APIDataService.CreateSession();
                objtkey = APIDataService.GetAPI(KEYID);

                objtkey.ACCOUNTID = ACCOUNTID;
                objtkey.EXCHANGE = EXCHANGE;
                objtkey.APIKEY = APIKEY;
                objtkey.APISECRET = APISECRET;
                objtkey.PASSPHRASE = PASSPHRASE;


                APIDataService.BeginTransaction();
                APIDataService.UpdateAPI(objtkey);
                APIDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("API KEY updated successfully.");

            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                APIDataService.CloseSession();
            }

            return objtkey;
        }


        public tkey GetAPI(int KeyID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            tkey objtkey = new tkey();

            try
            {
                APIDataService.CreateSession();
                objtkey = APIDataService.GetAPI(KeyID);

                if (objtkey != null)
                {
                    transaction.ReturnStatus = true;
                }
                else
                {
                    transaction.ReturnStatus = false;
                    transaction.ReturnMessage.Add("API KEYID not found.");
                }
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                APIDataService.CloseSession();
            }

            return objtkey;
        }


        public List<tkey> APIInquiry(string exchange, string apiKey, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<tkey> keyList = new List<tkey>();

            try
            {
                APIDataService.CreateSession();
                keyList = APIDataService.APIInquiry(exchange, apiKey, paging, out transaction);
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
                APIDataService.CloseSession();
            }

            return keyList;
        }

    }
}
