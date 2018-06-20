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
    public class ClientBusinessRules : ValidationRules
    {
        IClientDataService clientDataService;

        /// <summary>
        /// Initialize Client Business Rules
        /// </summary>
        /// <param name="objClient"></param>
        /// <param name="dataService"></param>
        public void InitializeClientBusinessRules(client objClient, IClientDataService dataService)
        {
            clientDataService = dataService;
            InitializeValidationRules(objClient);
        }

        public void ValidateClient(client objClient, IClientDataService dataService)
        {
            clientDataService = dataService;

            InitializeValidationRules(objClient);

            ValidateRequired("Title", "Organization Name");
            ValidateRequired("ContactPerson", "Contact Person Name");
            ValidateRequired("Email", "Email Address");
            
            ValidateEmailAddress("Email", "Email Address");

            ValidateUniqueOrganization(objClient.Title);
        }

        public void ValidateUniqueOrganization(string title)
        {
            IClientDataService dService = new ClientDataService();
            if (!string.IsNullOrEmpty(title))
            {
                client objCLient = dService.GetClientByTitle(title);
                if (objCLient != null)
                {
                    AddValidationError("Title", "- Organization '" + title + "' already exists. Please ask your account admin to create a user for you.");
                }
            }
        }

        public void ValidateUniqueClientCode(string code)
        {
            client objClient= clientDataService.GetClientByCode(code);
            if (objClient != null)
            {
                AddValidationError("Code", "- Client Code '" + code + "' already exists. Please choose a different code.");
            }

        }

    }
}
