using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;

namespace AppLibrary.Interfaces
{
    public interface IActivityLogDataService : IDataService, IDisposable
    {
        void AddActivityLog(activitylog objActivitylog);
        activitylog GetActivityLog(int ID);
        void UpdateActivityLog(activitylog objActivitylog);
    }
}
