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
    
    public partial class contact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public string Photo { get; set; }
        public Nullable<bool> AllowNewsLetter { get; set; }
        public string Comments { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Responded { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
