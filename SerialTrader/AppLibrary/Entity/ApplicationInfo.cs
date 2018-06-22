using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Common;
using AppLibrary.Model;

namespace AppLibrary.Entity
{
    public class ApplicationInfo: TransactionalInformation
    {
        public List<applicationmenu> MenuItems;
        public taccount account;

        public ApplicationInfo()
        {
            account = new taccount();
            MenuItems = new List<applicationmenu>();
        }
    }
}
