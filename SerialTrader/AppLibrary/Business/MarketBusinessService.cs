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
    public class MarketBusinessService : EntityFrameworkDataService
    {
        IMarketDataService _marketDataService;

        private IMarketDataService marketDataService
        {
            get { return _marketDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MarketBusinessService(IMarketDataService dataService)
        {
            _marketDataService = dataService;
        }

        public tmarket GetMarket(int MarketID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            tmarket market = new tmarket();

            try
            {
                marketDataService.CreateSession();
                market = marketDataService.GetMarket(MarketID);
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
                marketDataService.CloseSession();
            }
            return market;
        }

        public List<tmarket> GetMarkets(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<tmarket> marketList = new List<tmarket>();

            try
            {
                marketDataService.CreateSession();
                marketList = marketDataService.GetMarkets();
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
                marketDataService.CloseSession();
            }

            return marketList;
        }

    }
}
