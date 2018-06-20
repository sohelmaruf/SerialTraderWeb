using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;

namespace AppLibrary.DataServices
{
    public class PackageDataService : EntityFrameworkDataService, IPackageDataService
    {
        /// <summary>
        /// Create Package
        /// </summary>
        public void AddPackage(package objPackage)
        {
            dbConnection.packages.Add(objPackage);
        }

        public package GetPackage(int ID)
        {
            package objPackage = dbConnection.packages.SingleOrDefault(u => u.ID == ID);
            return objPackage;
        }

        public package GetPackageByName(string Name)
        {
            package objPackage = dbConnection.packages.SingleOrDefault(u => u.Name == Name);
            return objPackage;
        }
        
        public void UpdatePackage(package objPackage)
        {
            dbConnection.packages.Add(objPackage);
        }
    }
}
