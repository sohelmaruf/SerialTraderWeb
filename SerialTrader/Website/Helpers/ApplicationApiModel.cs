using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using AppLibrary.Model;

namespace Website.Helpers
{
   

    public class ApplicationApiModel : TransactionalInformation
    {
    
        public List<applicationmenu> MenuItems;

        public ApplicationApiModel()
        {           
            MenuItems = new List<applicationmenu>();        
        }

    }
}