using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;

namespace AppLibrary.Interfaces
{
    public interface IUserDataService : IDataService, IDisposable
    {
        void AddUser(taccount objuser);
        taccount GetUser(int ID);
        taccount GetUserByUserName(string userName);
        taccount Login(string userName, string password);
        void UpdateLastLogin(taccount user);
        void UpdateUser(taccount objuser);
    }
}
