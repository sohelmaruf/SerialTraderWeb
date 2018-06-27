using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Interfaces
{
    public interface IContactDataService : IDataService, IDisposable

    {
        void AddContact(contact contact);
        contact GetContact(int ID);
        void UpdateContact(contact contact);
        List<contact> ContactInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction);
    }
}
