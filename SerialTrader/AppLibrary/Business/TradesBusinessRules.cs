﻿using System;
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
    public class TradesBusinessRules : ValidationRules
    {
        ITradesDataService tradesDataService;

        /// <summary>
        /// Initialize user Business Rules
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void InitializeTradesBusinessRules(ttrade trade, ITradesDataService dataService)
        {
            tradesDataService = dataService;
            InitializeValidationRules(trade);

        }


        /// <summary>
        /// Validate Trade
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="dataService"></param>
        public void ValidateTrade(ttrade trade, ITradesDataService dataService)
        {
            tradesDataService = dataService;

            InitializeValidationRules(trade);

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
        /// Validate Trade Detail Line Item
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="dataService"></param>
        public void ValidateTradeDetailLineItem(ttrade trade, ITradesDataService dataService)
        {
            tradesDataService = dataService;
            InitializeValidationRules(trade);

            //ValidateGreaterThanZero("Quantity", "Order Quantity");
        }

    }
}
