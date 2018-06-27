using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Common;
using AppLibrary.Model;

namespace AppLibrary.Entity
{
    public class ContactInfo : TransactionalInformation
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public string Photo { get; set; }
        public Nullable<bool> AllowNewsLetter { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> Responded { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }

        public contact Contact = new contact();
        public List<contact> Contacts = new List<contact>();
    }
}
