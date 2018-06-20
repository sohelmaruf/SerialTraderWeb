using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Interfaces;
using AppLibrary.DataServices;
using AppLibrary.Model;

namespace AppLibrary.Common
{  
    public class Constants
    {
        public Constants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// WATERCONS MODULE CONSTANTS
        /// </summary>
        public const string WATERCONS_MODULE_MAIN = "Main";
        public const string WATERCONS_MODULE_ADMIN = "Admin";
        public const string WATERCONS_MODULE_ACCOUNTS = "Accounts";
        public const string WATERCONS_MODULE_PROPERTIES = "Properties";
        public const string WATERCONS_MODULE_PEOPLE = "People";
        public const string WATERCONS_MODULE_ORGANIZATIONS = "Organizations";
        public const string WATERCONS_MODULE_CASES = "Cases";
        public const string WATERCONS_MODULE_PROGRAMS = "Programs";
        public const string WATERCONS_MODULE_REPORTS = "Reports";
        public const string WATERCONS_MODULE_DASHBOARDs = "Dashboards";

        /// <summary>
        /// WATERCONS PACKAGE CONSTANTS
        /// </summary>
        public const string WATERCONS_PACKAGE_BASIC = "BASIC";
        public const string WATERCONS_PACKAGE_STANDARD = "STANDARD";
        public const string WATERCONS_PACKAGE_ENTERPRISE = "ENTERPRISE";


        /// <summary>
        /// WATERCONS ROLE CONSTANTS
        /// </summary>
        public const string WATERCONS_ROLE_SYSADMIN = "SysAdmin";
        public const string WATERCONS_ROLE_ADMIN = "Admin";
        public const string WATERCONS_ROLE_USER = "User";
        public const string WATERCONS_ROLE_INTERN = "Intern";


        public const string WATERCONS_APPLICATION_DEFAULT_PAGE = "Application.html#/Dashboard";

    }
}
