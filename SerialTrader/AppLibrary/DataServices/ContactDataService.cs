using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;

namespace AppLibrary.DataServices
{
    public class ContactDataService : EntityFrameworkDataService, IContactDataService
    {
        /// <summary>
        /// Create Contact
        /// </summary>
        public void AddContact(contact contact)
        {
            dbConnection.contacts.Add(contact);
        }

        public contact GetContact(int ID)
        {
            contact contact = dbConnection.contacts.SingleOrDefault(u => u.ID == ID);
            return contact;
        }

        public void UpdateContact(contact contact)
        {
            dbConnection.contacts.Add(contact);
        }

        public List<contact> ContactInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            List<contact> contactList = new List<contact>();

            int numberOfRows = 0;

            var customerQuery = dbConnection.contacts.AsQueryable();

            if (firstName != null && firstName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.FirstName.StartsWith(firstName));
            }

            if (lastName != null && lastName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.LastName.StartsWith(lastName));
            }

            numberOfRows = customerQuery.Count();
            customerQuery = customerQuery.OrderBy(con => con.FirstName);

            var contacts = customerQuery.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            foreach (var cont in contacts)
            {
                contactList.Add(cont);
            }

            transaction.TotalPages = paging.TotalPages;
            transaction.TotalRows = paging.TotalRows;
            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add(numberOfRows.ToString() + " contact found.");

            return contactList;
        }
    }
}
