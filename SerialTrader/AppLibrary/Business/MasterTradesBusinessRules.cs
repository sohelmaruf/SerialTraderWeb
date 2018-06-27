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
    public class MasterTradesBusinessRules : ValidationRules
    {
        IMasterTradesDataService masterTradesDataService;

        /// <summary>
        /// Initialize MasterTrade Business Rules
        /// </summary>
        /// <param name="masterTrade"></param>
        /// <param name="dataService"></param>
        public void InitializeMasterTradeBusinessRules(tmastertrade masterTrade, IMasterTradesDataService dataService)
        {
            masterTradesDataService = dataService;
            InitializeValidationRules(masterTrade);
        }


        /// <summary>
        /// Validate MasterTrade
        /// </summary>
        /// <param name="masterTrade"></param>
        /// <param name="dataService"></param>
        public void ValidateMasterTrade(tmastertrade masterTrade, IMasterTradesDataService dataService)
        {
            masterTradesDataService = dataService;

            InitializeValidationRules(masterTrade);

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
        /// Validate MasterTrade Detail Line Item
        /// </summary>
        /// <param name="masterTrade"></param>
        /// <param name="dataService"></param>
        public void ValidateMasterTradeDetailLineItem(tmastertrade masterTrade, IMasterTradesDataService dataService)
        {
            masterTradesDataService = dataService;
            InitializeValidationRules(masterTrade);

            //ValidateGreaterThanZero("Quantity", "Order Quantity");
        }
    }
}
