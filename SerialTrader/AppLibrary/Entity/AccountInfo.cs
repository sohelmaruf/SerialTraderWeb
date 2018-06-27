using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Entity
{
    public class AccountInfo : TransactionalInformation
    {
        public int AccountID { get; set; }	
	    public string FirstName { get; set; }
	    public string LastName { get; set; }
	    public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string IsEnabled { get; set; }
	    public string PostToSlack { get; set; }
	    public string SlackBotChannel { get; set; }
        public string Password{ get; set; }
        public string Role { get; set; }

        public taccount Account = new taccount();
        public List<taccount> Accounts = new List<taccount>();
    }

  /*
  public class AccountDetail
  {
      [Key]
      public Guid OrderDetailID { get; set; }
      public long OrderID { get; set; }
      public int LineItemNumber { get; set; }
      public Guid ProductID { get; set; }
      public int Quantity { get; set; }
      public DateTime? DateCreated { get; set; }
      public DateTime? DateUpdated { get; set; }
  }

  public class AccountInquiry
  {
      public Customer Customer;
      public Order Order;        
  }

  public class AccountDetails
  {
      public Customer Customer;
      public Order Order;
      public OrderDetail OrderDetail;
      public Product Product;
  }*/

}
