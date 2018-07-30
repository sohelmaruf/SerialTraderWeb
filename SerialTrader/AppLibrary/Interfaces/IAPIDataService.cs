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
    public interface IAPIDataService : IDataService, IDisposable
    {
        void AddAPI(tkey key);
        tkey GetAPI(int KEYID);
        void UpdateAPI(tkey key);
        List<tkey> APIInquiry(string exchange, string apiKey, DataGridPagingInformation paging, out TransactionalInformation transaction);
    }
}
