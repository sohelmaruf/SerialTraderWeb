using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class ExchangeInfo : TransactionalInformation
    {
        public int EXCHANGEID { get; set; }
        public string EXCHANGE { get; set; }
        public string TRADINGENABLED { get; set; }
        public string MARKETPAIRSENABLED { get; set; }

        public texchanx Exchange = new texchanx();
        public List<texchanx> Exchanges = new List<texchanx>();
    }
}
