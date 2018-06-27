using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class TrardeInfo : TransactionalInformation
    {
        public int TID { get; set; }
        public int ACCOUNTID { get; set; }
        public string EXCHANGE { get; set; }
        public int MASTERID { get; set; }
        public string TRADEGROUPID { get; set; }
        public string TRADINGACTION { get; set; }
        public string TRADINGPAIR { get; set; }
        public decimal TRADINGPRICE { get; set; }
        public decimal ACTUALRATE { get; set; }
        public decimal TRADINGQTY { get; set; }
        public decimal TOTAL { get; set; }
        public string ORDERID { get; set; }
        public string ORDERSTATUS { get; set; }
        public DateTime DATETIME { get; set; }
        public string ORDERRESULT { get; set; }
        public string RETRYIFCANCELED { get; set; }
        public string USEEXCHANGEPRICE { get; set; }
        public string ERRORRESULT { get; set; }

        public ttrade Trade = new ttrade();
        public List<ttrade> Trades = new List<ttrade>();
    }
}
