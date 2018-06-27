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
    public class TradesDataService : EntityFrameworkDataService, ITradesDataService
    {
        public void CreateTrade(ttrade trade)
        {
            dbConnection.ttrades.Add(trade);
        }
        
        public void UpdateTrade(ttrade trade)
        {
            //trade.DateUpdated = DateTime.Now;
        }

        public ttrade GetTrade(int TID)
        {
            ttrade trade = dbConnection.ttrades.SingleOrDefault(o => o.TID == TID);
            return trade;
        }

        public List<ttrade> TradeInquiry(string ACCOUNTID, string EXCHANGE, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            List<ttrade> tradeList = new List<ttrade>();

            int numberOfRows = 0;
            var customerQuery = dbConnection.ttrades.AsQueryable();

            if (ACCOUNTID != null && ACCOUNTID.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.ACCOUNTID.Equals(ACCOUNTID));
            }

            if (EXCHANGE != null && EXCHANGE.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.EXCHANGE.StartsWith(EXCHANGE));
            }
            
            numberOfRows = customerQuery.Count();
            customerQuery = customerQuery.OrderBy(ord => ord.TID);

            var trades = customerQuery.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            foreach (var obj in trades)
            {
                tradeList.Add(obj);
            }

            transaction.TotalPages = paging.TotalPages;
            transaction.TotalRows = paging.TotalRows;
            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add(numberOfRows.ToString() + " trade found.");

            return tradeList;
        }

    }
}
