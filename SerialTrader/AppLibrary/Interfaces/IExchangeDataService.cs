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
    public interface IExchangeDataService : IDataService, IDisposable
    {
        texchanx GetExchange(int ExchangeID);
        List<texchanx> GetExchanges();
    }
}
