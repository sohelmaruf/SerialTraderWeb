using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

using AppLibrary.Model;
using AppLibrary.Interfaces;
using AppLibrary.Common;
using AppLibrary.Entity;
using AppLibrary.DataServices;

namespace AppLibrary.Business
{
    public class AccountsBusinessService: EntityFrameworkDataService
    {
        IAccountsDataService _accountsDataService;

        private IAccountsDataService accountsDataService
        {
            get { return _accountsDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountsBusinessService(IAccountsDataService dataService)
        {
            _accountsDataService = dataService;
        }


        public taccount CreateAccount(int AccountID, string FirstName, string LastName, string EmailAddress,
                            string IsEnabled, string PostToSlack, string SlackBotChannel, string Password, string Role, 
                                out TransactionalInformation transaction)
        {
       
            transaction = new TransactionalInformation();
            AccountsBusinessRules accountsBusinessRules = new AccountsBusinessRules();
            taccount account = new taccount();

            account.ACCOUNTID = AccountID;
            account.FIRSTNAME = FirstName;
            account.LASTNAME = LastName;
            account.EMAILADDRESS = EmailAddress;
            account.ISENABLED = IsEnabled;
            account.POSTTOSLACK = PostToSlack;
            account.SLACKBOTCHANNEL = SlackBotChannel;
            account.PASSWORD = Password;
            account.ROLE = Role;
            
            try
            {

                accountsDataService.CreateSession();
                accountsBusinessRules.ValidateAccount(account, accountsDataService);

                if (accountsBusinessRules.ValidationStatus == true)
                {
                    accountsDataService.BeginTransaction();
                    accountsDataService.CreateAccount(account);
                    accountsDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Account successfully created.");
                }
                else
                {
                    transaction.ReturnStatus = accountsBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = accountsBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = accountsBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }

            return account;
        }


        public taccount UpdateAccount(int AccountID, string FirstName, string LastName, string EmailAddress,
                            string IsEnabled, string PostToSlack, string SlackBotChannel, string Password, string Role,
                                out TransactionalInformation transaction)

        {

            transaction = new TransactionalInformation();
            AccountsBusinessRules accountsBusinessRules = new AccountsBusinessRules();
            taccount account = dbConnection.taccounts.Where(o => o.ACCOUNTID == AccountID).FirstOrDefault();
            //taccount account = new taccount();

            try
            {
                //accountsDataService.CreateSession();

                //account = accountsDataService.GetAccount(AccountID);
                account.FIRSTNAME = FirstName;
                account.LASTNAME = LastName;
                account.EMAILADDRESS = EmailAddress;
                //account.ISENABLED = IsEnabled;
                //account.POSTTOSLACK = PostToSlack;

                account.ISENABLED = "true";
                account.POSTTOSLACK = "true";

                account.SLACKBOTCHANNEL = SlackBotChannel;
                account.PASSWORD = Password;
                account.ROLE = Role;

                accountsBusinessRules.ValidateAccount(account, accountsDataService);

                if (accountsBusinessRules.ValidationStatus == true)
                {
                    accountsDataService.BeginTransaction();

                    dbConnection.SaveChanges();
                    //accountsDataService.UpdateAccount(account);
                    accountsDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Account successfully updated at " + DateTime.Now.ToShortDateString());
                }
                else
                {
                    transaction.ReturnStatus = accountsBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = accountsBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = accountsBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }

            return account;
        }        
   
        
        public taccount GetAccount(int AccountID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            taccount account = new taccount();

            try
            {
                accountsDataService.CreateSession();
                account = accountsDataService.GetAccount(AccountID);
                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }
            return account;
        }
        
        public List<taccount> GetAccountDetails(int AccountID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<taccount> accountList = new List<taccount>();

            try
            {
                accountsDataService.CreateSession();
                accountList = accountsDataService.GetAccountDetails(AccountID);
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }

            return accountList;
        }
        
        public List<taccount> AccountInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<taccount> accountList = new List<taccount>();

            try
            {
                accountsDataService.CreateSession();
                accountList = accountsDataService.AccountInquiry(firstName, lastName, paging, out transaction);
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }

            return accountList;
        }

        public taccount CreateAccountDetailLineItem(int AccountID, int quantity, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            AccountsBusinessRules accountsBusinessRules = new AccountsBusinessRules();
            taccount account = new taccount();
            
            try
            {
                accountsDataService.CreateSession();
                accountsBusinessRules.ValidateAccountDetailLineItem(account, accountsDataService);

                if (accountsBusinessRules.ValidationStatus == true)
                {
                    accountsDataService.BeginTransaction();
                    accountsDataService.CreateAccountDetailLineItem(account);
                    accountsDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Account line item successfully created.");
                }
                else
                {
                    transaction.ReturnStatus = accountsBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = accountsBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = accountsBusinessRules.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }
            return account;
        }

        public taccount UpdateAccountDetailLineItem(int AccountID, int quantity, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            AccountsBusinessRules accountsBusinessRules = new AccountsBusinessRules();

            taccount account = new taccount();
            
            try
            {
                accountsDataService.CreateSession();
                accountsBusinessRules.ValidateAccountDetailLineItem(account, accountsDataService);

                if (accountsBusinessRules.ValidationStatus == true)
                {
                    accountsDataService.BeginTransaction();
                    accountsDataService.UpdateAccountDetailLineItem(account);
                    accountsDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Account line item successfully updated.");
                }
                else
                {
                    transaction.ReturnStatus = accountsBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = accountsBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = accountsBusinessRules.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }
            return account;
        }

        
        public void DeleteAccountDetailLineItem(int AccountID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            AccountsBusinessRules accountsBusinessRules = new AccountsBusinessRules();
       
            try
            {
                accountsDataService.CreateSession();
                accountsDataService.BeginTransaction();
                accountsDataService.DeleteAccountDetailLineItem(AccountID);
                accountsDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;            
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                accountsDataService.CloseSession();
            }         
        }
    }

}

