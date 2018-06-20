using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using WaterCons.Models;

namespace WaterCons.Helpers
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