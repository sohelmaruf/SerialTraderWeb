using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.DataServices;

namespace AppLibrary.Business
{
    public class ActivityLogBusinessService
    {
        IActivityLogDataService _ActivityLogDataService;

        private IActivityLogDataService ActivityLogDataService
        {
            get { return _ActivityLogDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityLogBusinessService(IActivityLogDataService dataService)
        {
            _ActivityLogDataService = dataService;
        }

        public activitylog AddActivityLog(activitylog objActivityLog)
        {
            try
            {
                ActivityLogDataService.CreateSession();
                ActivityLogDataService.BeginTransaction();
                ActivityLogDataService.AddActivityLog(objActivityLog);
                ActivityLogDataService.CommitTransaction(true);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ActivityLogDataService.CloseSession();
            }

            return objActivityLog;
        }

        
        public activitylog GetActivityLog(int ID)
        {

            activitylog objActivityLog = new activitylog();

            try
            {
                ActivityLogDataService.CreateSession();
                objActivityLog = ActivityLogDataService.GetActivityLog(ID);   
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ActivityLogDataService.CloseSession();
            }

            return objActivityLog;
        }

    }
}
