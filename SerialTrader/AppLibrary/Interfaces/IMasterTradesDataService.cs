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
    public interface IMasterTradesDataService : IDataService, IDisposable
    {
        void CreateMasterTrade(tmastertrade masterTrade);
        void UpdateMasterTrade(tmastertrade masterTrade);
        tmastertrade GetMasterTrade(int MASTERID);
        List<tmastertrade> MasterTradeInquiry(string ACCOUNTID, string EXCHANGE, DataGridPagingInformation paging, out TransactionalInformation transaction);
    }
}
