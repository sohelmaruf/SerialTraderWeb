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
        void RegisterUser(user user);
        user GetUserByUserName(string userName);
        user Login(string userName, string password);
        void UpdateLastLogin(user user);
        user GetUser(int userID);
        void UpdateUser(user user);
    }
}
