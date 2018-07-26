using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;
using System.Web.UI.WebControls;

namespace AppLibrary.DataServices
{
    public class AccountsDataService : EntityFrameworkDataService, IAccountsDataService
    {
        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="Account"></param>
        public void CreateAccount(taccount account)
        {
            dbConnection.taccounts.Add(account);

        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="Account"></param>
        public void UpdateAccount(taccount account)
        {
            dbConnection.SaveChanges();
        }

        /// <summary>
        /// Create Account Detail Line Item
        /// </summary>
        /// <param name="accountDetail"></param>
        public void CreateAccountDetailLineItem(taccount objAccount)
        {

            int count = dbConnection.taccounts.Where(o => o.ACCOUNTID == objAccount.ACCOUNTID).Count();
            int itemNumber = 0;
            //if (count > 0)
            //{
            //    var maxItemNumber = dbConnection.taccounts.Where(o => o.ACCOUNTID== account.ACCOUNTID).Max(i => (int?)i.LineItemNumber ?? 0);                      
            //    itemNumber = Convert.ToInt32(maxItemNumber);               
            //}

            //itemNumber++;

            //DateTime accountCreated = DateTime.Now;

            //orderDetail.OrderDetailID = Guid.NewGuid();
            //orderDetail.LineItemNumber = itemNumber;
            //orderDetail.DateCreated = orderCreated;
            //orderDetail.DateUpdated = orderCreated;

            //dbConnection.OrderDetails.Add(orderDetail);

            //decimal accountTotal = 0;

            //if (count > 0)
            //{
            //    accountTotal = (from taccount in dbConnection.taccounts.Where(o => o.ACCOUNTID== account.ACCOUNTID)
            //                  join products in dbConnection.Products on orderDetails.ProductID equals products.ProductID
            //                  select new { products.UnitPrice, orderDetails.Quantity }).Sum(o => o.Quantity * o.UnitPrice);

            //}
           
            //Product product = dbConnection.Products.SingleOrDefault(p => p.ProductID == orderDetail.ProductID);
            //orderTotal = orderTotal + ( orderDetail.Quantity * product.UnitPrice );                           
          
            taccount account = dbConnection.taccounts.Where(o => o.ACCOUNTID == objAccount.ACCOUNTID).FirstOrDefault();
            //order.OrderTotal = orderTotal;
                     
        }


        /// <summary>
        /// Update Account Detail Line Item
        /// </summary>
        /// <param name="AccountDetail"></param>
        public void UpdateAccountDetailLineItem(taccount objAccount)
        {
            
            taccount account = dbConnection.taccounts.SingleOrDefault(o => o.ACCOUNTID == objAccount.ACCOUNTID);

            //int originalQuantity = order.Quantity;
            
            //order.Quantity = orderDetail.Quantity;
            //order.DateUpdated = DateTime.Now;     

            //Product product = dbConnection.Products.SingleOrDefault(p => p.ProductID == order.ProductID);
            //decimal originalAmount = (originalQuantity * product.UnitPrice);
            //decimal newAmount = (order.Quantity * product.UnitPrice);
            //decimal diff = newAmount - originalAmount;

            //Order orderHeader = dbConnection.Orders.Where(o => o.OrderID == orderDetail.OrderID).FirstOrDefault();
            //orderHeader.OrderTotal = orderHeader.OrderTotal + diff;

        }

        /// <summary>
        /// Delete Account Detail Line Item
        /// </summary>
        /// <param name="AccountID"></param>
        public void DeleteAccountDetailLineItem(int AccountID)
        {

            taccount account = dbConnection.taccounts.SingleOrDefault(o => o.ACCOUNTID== AccountID);
   
            //Product product = dbConnection.Products.SingleOrDefault(p => p.ProductID == order.ProductID);
            //decimal amount = (order.Quantity * product.UnitPrice);
   
            //Order orderHeader = dbConnection.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();
            //orderHeader.OrderTotal = orderHeader.OrderTotal - amount;

            dbConnection.taccounts.Remove(account);

        }
        
        /// <summary>
        /// Get Shippers
        /// </summary>
        /// <returns></returns>
        //public List<Shipper> GetShippers()
        //{

        //    var shipperQuery = dbConnection.Shippers.AsQueryable();
        //    var shippers = (from s in shipperQuery.OrderBy("ShipperName") select s).ToList();
        //    return shippers;

        //}

        /// <summary>
        /// Get Shippers
        /// </summary>
        /// <returns></returns>
        public List<taccount> GetAccountDetails(int AccountID)
        {
            taccount account = dbConnection.taccounts.SingleOrDefault(o => o.ACCOUNTID == AccountID);
            //return account;

            //var orderItems = from order in dbConnection.Orders.Where(o => o.OrderID == orderID)
            //                  join orderDetail in dbConnection.OrderDetails on order.OrderID equals orderDetail.OrderID
            //                  join product in dbConnection.Products on orderDetail.ProductID equals product.ProductID                              
            //                  select new { product, order, orderDetail };

            //orderItems = orderItems.OrderBy(o => o.orderDetail.LineItemNumber);

            List<taccount> accountList = new List<taccount>();
            accountList.Add(account);

            //foreach (var orderDetail in orderItems)
            //{
            //    OrderDetails orderDetails = new OrderDetails();
            //    orderDetails.Product = orderDetail.product;
            //    orderDetails.Order = orderDetail.order;
            //    orderDetails.OrderDetail = orderDetail.orderDetail;

            //    listOfOrderDetails.Add(orderDetails);
            //}

            return accountList;

        }

        /// <summar
        /// <summary>
        /// Get Account
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public taccount GetAccount(int AccountID)
        {          
            taccount account = dbConnection.taccounts.SingleOrDefault(o => o.ACCOUNTID == AccountID);
            return account;
        }
        
        public List<taccount> AccountInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            List<taccount> accountList = new List<taccount>();

            int numberOfRows = 0;

            var customerQuery = dbConnection.taccounts.AsQueryable();

            if (firstName != null && firstName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.FIRSTNAME.StartsWith(firstName));
            }

            if (lastName != null && lastName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.LASTNAME.StartsWith(lastName));
            }

            //var orders = from customer in customerQuery
            //                join order in dbConnection.Orders on customer.CustomerID equals order.CustomerID
            //                select new { customer, order, order.OrderDate, order.OrderTotal, customer.CustomerCode, customer.CompanyName };                            

            numberOfRows = customerQuery.Count();

            customerQuery = customerQuery.OrderBy(acc => acc.FIRSTNAME);

            var accounts = customerQuery.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            foreach (var account in accounts)
            {
                accountList.Add(account);
            }
            
            transaction.TotalPages = paging.TotalPages;
            transaction.TotalRows = paging.TotalRows;
            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add(numberOfRows.ToString() + " account found.");

            return accountList;
        }

    }
}

