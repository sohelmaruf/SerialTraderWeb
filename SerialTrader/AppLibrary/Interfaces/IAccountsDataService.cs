using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Entity;
using AppLibrary.Model;
using AppLibrary.Common;

namespace AppLibrary.Interfaces
{
    public interface IAccountsDataService : IDataService, IDisposable
    {
        void CreateAccount(taccount objAccount);
        void UpdateAccount(taccount objAccount);
        //List<Shipper> GetShippers();
        taccount GetAccount(int AccountID);
        List<taccount> AccountInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction);
        void CreateAccountDetailLineItem(taccount objAccount);
        void UpdateAccountDetailLineItem(taccount objAccount);
        List<taccount> GetAccountDetails(int AccountID);
        void DeleteAccountDetailLineItem(int AccountID);
    }
}
