using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;

namespace AppLibrary.Business
{
    public class AccountsBusinessRules : ValidationRules
    {

        IAccountsDataService accountsDataService;

        /// <summary>
        /// Initialize Account Business Rules
        /// </summary>
        /// <param name="account"></param>
        /// <param name="dataService"></param>
        public void InitializeAccountsBusinessRules(taccount account, IAccountsDataService dataService)
        {
            accountsDataService = dataService;
            InitializeValidationRules(account);
        }

       
        /// <summary>
        /// Validate Account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="dataService"></param>
        public void ValidateAccount(taccount account, IAccountsDataService dataService)
        {
            accountsDataService = dataService;
            InitializeValidationRules(account);
            ValidateEmailAddress("EMAILADDRESS", "Email Address");

            //ValidateRequired("ShipName", "Ship To Name");
            //ValidateRequired("ShipCity", "Ship To City");
            //ValidateRequired("ShipRegion", "Ship To Region");
            //ValidateRequired("ShipPostalCode", "Ship To Postal Code");
            //ValidateRequired("ShipCountry", "Ship To Country");
            //ValidateRequired("ShipAddress", "Ship To Address");                            
            //ValidateRequiredDate("RequiredDate", "Required Ship Date");
            //ValidateSelectedValue("ShipVia", "Ship Via");
        }

        /// <summary>
        /// Validate Account Detail Line Item
        /// </summary>
        /// <param name="account"></param>
        /// <param name="dataService"></param>
        public void ValidateAccountDetailLineItem(taccount account, IAccountsDataService dataService)
        {
            accountsDataService = dataService;
            InitializeValidationRules(account);

            //ValidateGreaterThanZero("Quantity", "Order Quantity");
        }


    }
}

