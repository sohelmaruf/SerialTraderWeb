using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;

namespace AppLibrary.DataServices
{
    public class MasterTradesDataService : EntityFrameworkDataService, IMasterTradesDataService
    {
        public void CreateMasterTrade(tmastertrade masterTrade)
        {
            dbConnection.tmastertrades.Add(masterTrade);
        }

        public void UpdateMasterTrade(tmastertrade masterTrade)
        {
            //trade.DateUpdated = DateTime.Now;
        }

        public tmastertrade GetMasterTrade(int MASTERID)
        {
            tmastertrade masterTrade = dbConnection.tmastertrades.SingleOrDefault(o => o.MASTERID == MASTERID);
            return masterTrade;
        }

        public List<tmastertrade> MasterTradeInquiry(string TradeType, string TradingPair, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            List<tmastertrade> masterTradeList = new List<tmastertrade>();

            int numberOfRows = 0;
            var customerQuery = dbConnection.tmastertrades.AsQueryable();

            if (TradeType != null && TradeType.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.TRADETYPE.StartsWith(TradeType));
            }

            if (TradingPair != null && TradingPair.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.TRADINGPAIR.StartsWith(TradingPair));
            }

            numberOfRows = customerQuery.Count();
            customerQuery = customerQuery.OrderBy(ord => ord.MASTERID);

            var trades = customerQuery.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            foreach (var obj in trades)
            {
                masterTradeList.Add(obj);
            }

            transaction.TotalPages = paging.TotalPages;
            transaction.TotalRows = paging.TotalRows;
            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add(numberOfRows.ToString() + " mastertrade found.");

            return masterTradeList;
        }
    }
}
