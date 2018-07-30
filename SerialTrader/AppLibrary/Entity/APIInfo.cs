using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class APIInfo : TransactionalInformation
    {
        public int KEYID { get; set; }
        public int ACCOUNTID { get; set; }
        public string EXCHANGE { get; set; }
        public string APIKEY { get; set; }
        public string APISECRET { get; set; }
        public string PASSPHRASE { get; set; }

        public tkey Key = new tkey();
        public List<tkey> Keys = new List<tkey>();
        public List<texchanx> Exchanges = new List<texchanx>();
    }
}
