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
    
    public partial class tprice
    {
        public long PriceID { get; set; }
        public string Exchange { get; set; }
        public string Market { get; set; }
        public Nullable<decimal> LastPrice { get; set; }
        public Nullable<decimal> Bid { get; set; }
        public Nullable<decimal> Buy { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    }
}
