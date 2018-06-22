using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Interfaces;
using AppLibrary.Model;

namespace AppLibrary.Interfaces
{
    public interface IAdminDataService : IDataService, IDisposable
    {
        void RegisterUser(taccount user);
        taccount GetUserByUserName(string userName);
        taccount Login(string userName, string password);
        void UpdateLastLogin(taccount user);
        taccount GetUser(int userID);
        void UpdateUser(taccount user);
    }
}
