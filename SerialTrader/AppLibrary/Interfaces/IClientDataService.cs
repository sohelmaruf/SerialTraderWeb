using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;

namespace AppLibrary.Interfaces
{
    public interface IClientDataService : IDataService, IDisposable
    {
        void AddClient(client objclient);
        client GetClient(int ID);
        client GetClientByTitle(string title);
        client GetClientByCode(string code);
        void UpdateClient(client objclient);
    }
}
