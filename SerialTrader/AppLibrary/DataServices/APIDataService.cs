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
    public class APIDataService : EntityFrameworkDataService, IAPIDataService
    {
        /// <summary>
        /// Create API
        /// </summary>
        public void AddAPI(tkey key)
        {
            dbConnection.tkeys.Add(key);
        }

        public tkey GetAPI(int keyID)
        {
            tkey key = dbConnection.tkeys.SingleOrDefault(u => u.KEYID == keyID);
            return key;
        }

        public void UpdateAPI(tkey key)
        {
            dbConnection.tkeys.Add(key);
        }

        public List<tkey> APIInquiry(string exchange, string apiKey, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            List<tkey> keyList = new List<tkey>();

            int numberOfRows = 0;

            var customerQuery = dbConnection.tkeys.AsQueryable();

            if (exchange != null && exchange.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.EXCHANGE.StartsWith(exchange));
            }

            if (apiKey != null && apiKey.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.APIKEY.StartsWith(apiKey));
            }

            numberOfRows = customerQuery.Count();
            customerQuery = customerQuery.OrderBy(con => con.KEYID);

            var keys = customerQuery.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            foreach (var key in keys)
            {
                keyList.Add(key);
            }

            transaction.TotalPages = paging.TotalPages;
            transaction.TotalRows = paging.TotalRows;
            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add(numberOfRows.ToString() + " API Key found.");

            return keyList;
        }
    }
}
