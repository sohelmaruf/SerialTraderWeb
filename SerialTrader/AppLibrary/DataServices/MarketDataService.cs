using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;

namespace AppLibrary.DataServices
{
    public class MarketDataService : EntityFrameworkDataService, IMarketDataService
    {
        public tmarket GetMarket(int MarketID)
        {
            tmarket market = dbConnection.tmarkets.SingleOrDefault(o => o.MarketID == MarketID);
            return market;
        }


        public List<tmarket> GetMarkets()
        {
            List<tmarket> marketList = new List<tmarket>();

            foreach (var market in dbConnection.tmarkets)
            {
                marketList.Add(market);

            }

            return marketList;
        }
    }
}
