using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.DataServices;

namespace AppLibrary.DataServices
{
  
    public class EntityFrameworkDataService : IDataService, IDisposable
    {

        private serialtraderEntities db = new serialtraderEntities();


        serialtraderEntities _connection = new serialtraderEntities();

        public serialtraderEntities dbConnection
        {
            get { return _connection; }
        }


        public void CommitTransaction(Boolean closeSession)
        {
            dbConnection.SaveChanges();
        }

        public void RollbackTransaction(Boolean closeSession)
        {

        }

        public void Save(object entity) { }

        public void CreateSession() {
       
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AngularJSDatabase,  Configuration>()); 
 
            //_connection = new AngularJSDatabase();
        }
        public void BeginTransaction() { }

        public void CloseSession() { }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
        }
    }
}
