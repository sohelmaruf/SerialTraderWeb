//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppLibrary.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ttrade
    {
        public long TID { get; set; }
        public long ACCOUNTID { get; set; }
        public string EXCHANGE { get; set; }
        public Nullable<long> MASTERID { get; set; }
        public string TRADEGROUPID { get; set; }
        public string TRADINGACTION { get; set; }
        public string TRADINGPAIR { get; set; }
        public decimal TRADINGPRICE { get; set; }
        public Nullable<decimal> ACTUALRATE { get; set; }
        public Nullable<decimal> TRADINGQTY { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public string ORDERID { get; set; }
        public string ORDERSTATUS { get; set; }
        public System.DateTime DATETIME { get; set; }
        public string ORDERRESULT { get; set; }
        public string RETRYIFCANCELED { get; set; }
        public string USEEXCHANGEPRICE { get; set; }
        public string ERRORRESULT { get; set; }
    }
}