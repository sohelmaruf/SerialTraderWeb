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
    public class ExchangeDataService : EntityFrameworkDataService, IExchangeDataService
    {

        public texchanx GetExchange(int ExchangeID)
        {
            texchanx exchange = dbConnection.texchanges.SingleOrDefault(o => o.EXCHANGEID == ExchangeID);
            return exchange;
        }


        public List<texchanx> GetExchanges()
        {
            List<texchanx> exchangeList = new List<texchanx>();

            foreach (var exchange in dbConnection.texchanges)
            {
                exchangeList.Add(exchange);

            }

            return exchangeList;
        }
    }
}
