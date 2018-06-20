using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;


namespace AppLibrary.Business
{
    public class ContactBusinessRules : ValidationRules
    {
        IContactDataService contactDataService;

        /// <summary>
        /// Initialize Contact Business Rules
        /// </summary>
        /// <param name="objContact"></param>
        /// <param name="dataService"></param>
        public void InitializeContactBusinessRules(contact objContact, IContactDataService dataService)
        {
            contactDataService = dataService;
            InitializeValidationRules(objContact);
        }

        public void ValidateContact(contact objContact, IContactDataService dataService)
        {
            contactDataService = dataService;

            InitializeValidationRules(objContact);
            ValidateEmailAddress("Email", "Email Address");
        }

    }
}
