using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Entity;
using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Interfaces
{
    public interface ITradesDataService : IDataService, IDisposable
    {
        void CreateTrade(ttrade trade);
        void UpdateTrade(ttrade trade);
        ttrade GetTrade(int TID);
        List<ttrade> TradeInquiry(string ACCOUNTID, string EXCHANGE, DataGridPagingInformation paging, out TransactionalInformation transaction);
    }
}
