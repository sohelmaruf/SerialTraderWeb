using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class MarketInfo : TransactionalInformation
    {
        public int MarketID { get; set; }
        public string EXCHANGE { get; set; }
        public string EXCHANGEMARKET { get; set; }
        public string STMARKET { get; set; }
        public string BASECURRENCY { get; set; }
        public string MARKETCURRENCY { get; set; }
        
        public tmarket Market = new tmarket();
        public List<tmarket> Markets = new List<tmarket>();
    }
}
