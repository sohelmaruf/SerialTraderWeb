using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;

namespace AppLibrary.Business
{
    public class APIBusinessRules : ValidationRules
    {
        IAPIDataService apiDataService;

        /// <summary>
        /// Initialize API Business Rules
        /// </summary>
        /// <param name="objtkey"></param>
        /// <param name="dataService"></param>
        public void InitializeAPIBusinessRules(tkey objtkey, IAPIDataService dataService)
        {
            apiDataService = dataService;
            InitializeValidationRules(objtkey);
        }

        public void ValidateAPI(tkey objtkey, IAPIDataService dataService)
        {
            apiDataService = dataService;

            InitializeValidationRules(objtkey);
            ValidateEmailAddress("Email", "Email Address");
        }

    }
}
