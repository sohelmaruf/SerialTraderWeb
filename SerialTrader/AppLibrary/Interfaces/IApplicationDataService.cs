using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Interfaces;
using AppLibrary.Model;

namespace AppLibrary.Interfaces
{
    public interface IApplicationDataService : IDataService, IDisposable
    {
        void InitializeApplication();
        List<applicationmenu> GetMenuItems(Boolean isAuthenicated);
    }
}
