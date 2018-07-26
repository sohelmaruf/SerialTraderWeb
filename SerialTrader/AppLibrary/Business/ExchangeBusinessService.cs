using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;
using AppLibrary.DataServices;

namespace AppLibrary.Business
{
    public class ExchangeBusinessService : EntityFrameworkDataService
    {
        IExchangeDataService _exchangeDataService;

        private IExchangeDataService exchangeDataService
        {
            get { return _exchangeDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeBusinessService(IExchangeDataService dataService)
        {
            _exchangeDataService = dataService;
        }

        public texchanx GetExchange(int ExchangeID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            texchanx exchange = new texchanx();

            try
            {
                exchangeDataService.CreateSession();
                exchange = exchangeDataService.GetExchange(ExchangeID);
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
                exchangeDataService.CloseSession();
            }
            return exchange;
        }

        public List<texchanx> GetExchanges(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<texchanx> exchangeList = new List<texchanx>();

            try
            {
                exchangeDataService.CreateSession();
                exchangeList = exchangeDataService.GetExchanges();
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
                exchangeDataService.CloseSession();
            }

            return exchangeList;
        }

    }
}
