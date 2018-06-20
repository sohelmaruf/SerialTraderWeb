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
    public class ClientBusinessService
    {
        IClientDataService _ClientDataService;

        private IClientDataService ClientDataService
        {
            get { return _ClientDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientBusinessService(IClientDataService dataService)
        {
            _ClientDataService = dataService;
        }
        
        public client AddClient(string subscriptionType, string title, string code, string address, Nullable<int> contactID, string contactPerson, string contactNumber, string logo, string email,
            Nullable<bool> isActive, Nullable<bool> termsAccepted, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            ClientBusinessRules clientRules = new ClientBusinessRules();
            IPackageDataService packageDataService = new PackageDataService();
            
            client objClient = new client();

            try
            {
                package objPackage = packageDataService.GetPackageByName(subscriptionType);

                objClient.PackageID = objPackage.ID;
                objClient.Title = title;
                objClient.Code = code;
                objClient.Address = address;
                objClient.ContactID = contactID;
                objClient.ContactPerson = contactPerson;
                objClient.ContactNumber = contactNumber;
                objClient.Logo = logo;
                objClient.Email = email;
                objClient.IsActive = isActive;
                objClient.TermsAccepted = termsAccepted;
                objClient.CreateDate = System.DateTime.Now;
                objClient.CreatedBy = null;
                objClient.ModDate = System.DateTime.Now;
                objClient.ModBy = null;


                ClientDataService.CreateSession();
                ClientDataService.BeginTransaction();
                ClientDataService.AddClient(objClient);
                ClientDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Client added successfully.");
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ClientDataService.CloseSession();
            }

            return objClient;
        }


        public client UpdateClient(int ID, string title, string code, string address, Nullable<int> contactID, string contactPerson, string contactNumber, string logo, string email,
            Nullable<bool> isActive, Nullable<bool> termsAccepted, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();
            client objClient = new client();

            try
            {
                ClientDataService.CreateSession();

                objClient = ClientDataService.GetClient(ID);

                objClient.Title = title;
                objClient.Code = code;
                objClient.Address = address;
                objClient.ContactID = contactID;
                objClient.ContactPerson = contactPerson;
                objClient.ContactNumber = contactNumber;
                objClient.Logo = logo;
                objClient.Email = email;
                objClient.IsActive = isActive;
                objClient.TermsAccepted = termsAccepted;
                objClient.CreateDate = System.DateTime.Now;
                objClient.CreatedBy = null;
                objClient.ModDate = System.DateTime.Now;
                objClient.ModBy = null;


                ClientDataService.BeginTransaction();
                ClientDataService.AddClient(objClient);
                ClientDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Client updated successfully.");

            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ClientDataService.CloseSession();
            }

            return objClient;
        }


        public client GetClient(int ID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            client objClient = new client();

            try
            {
                ClientDataService.CreateSession();
                objClient = ClientDataService.GetClient(ID);

                if (objClient != null)
                {
                    transaction.ReturnStatus = true;
                }
                else
                {
                    transaction.ReturnStatus = false;
                    transaction.ReturnMessage.Add("Client id not found.");
                }
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ClientDataService.CloseSession();
            }

            return objClient;
        }

    }
}
