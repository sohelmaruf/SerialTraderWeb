﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class AdminInfo : TransactionalInformation
    {
        public List<applicationmenu> MenuItems;
        public taccount account;

        public AdminInfo()
        {
            account = new taccount();
            MenuItems = new List<applicationmenu>();
        }
    }
}
