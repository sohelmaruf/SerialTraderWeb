using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class MasterTradeInfo : TransactionalInformation
    {
        public int MASTERID { get; set; }
        public int ACCOUNTID { get; set; }
        public string EXCHANGE { get; set; }
        public string TRADETYPE { get; set; }
        public string TRADINGPAIR { get; set; }
        public string BUYPRICE { get; set; }
        public string BUYQUANTITY { get; set; }
        public decimal BUYTOTAL { get; set; }
        public string SELLPRICE { get; set; }
        public string SELLQUANTITY { get; set; }
        public string FIRSTACTION { get; set; }
        public DateTime LASTRUN { get; set; }
        public DateTime NEXTRUN { get; set; }
        public int RUNFREQUENCY { get; set; }
        public int RUNLIMIT { get; set; }
        public int RUNCOUNT { get; set; }
        public string ACTIVE { get; set; }
        public string SELLTOTAL { get; set; }

        public tmastertrade MasterTrade = new tmastertrade();
        public List<tmastertrade> MasterTrades = new List<tmastertrade>();
    }
}
