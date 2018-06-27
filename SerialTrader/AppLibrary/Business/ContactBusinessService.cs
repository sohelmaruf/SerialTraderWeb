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
    public class ContactBusinessService
    {
        IContactDataService _ContactDataService;

        private IContactDataService ContactsDataService
        {
            get { return _ContactDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactBusinessService(IContactDataService dataService)
        {
            _ContactDataService = dataService;
        }
        
        public contact AddContact(string firstName, string lastName, string email, string address, string homePhone, string officePhone, string street, string city,
            string zipCode, string state, string country, string organization, string designation, string photo,Nullable<bool> allowNewsLetter, string Comment, 
            out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            ContactBusinessRules contactRules = new ContactBusinessRules();
            
            contact objContact = new contact();

            try
            {

                //if(!string.IsNullOrEmpty(objContact.FirstName)) objContact.FirstName = Utilities.UppercaseFirstLetter(firstName.Trim());
                //if(!string.IsNullOrEmpty(objContact.LastName)) objContact.LastName = Utilities.UppercaseFirstLetter(lastName.Trim());
                objContact.FirstName = firstName;
                objContact.LastName = lastName;
                objContact.Email = email;
                objContact.Address = address;
                objContact.HomePhone = homePhone;
                objContact.OfficePhone = officePhone;
                objContact.Street = street;
                objContact.City = city;
                objContact.Zipcode = zipCode;
                objContact.State = state;
                objContact.Country = country;
                objContact.Organization = organization;
                objContact.Designation = designation;
                objContact.Photo = photo;
                objContact.AllowNewsLetter = allowNewsLetter;
                objContact.Comments = Comment;
                objContact.Responded = false;
                objContact.CreateDate = System.DateTime.Now;

                ContactsDataService.CreateSession();
                contactRules.ValidateContact(objContact, ContactsDataService);

                if (contactRules.ValidationStatus == true)
                {
                    ContactsDataService.BeginTransaction();
                    ContactsDataService.AddContact(objContact);
                    ContactsDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Contact added successfully.");
                }
                else
                {
                    transaction.ReturnStatus = contactRules.ValidationStatus;
                    transaction.ReturnMessage = contactRules.ValidationMessage;
                    transaction.ValidationErrors = contactRules.ValidationErrors;
                }
                //Send Email
                //EmailSender.SendEmail("sohel.maruf@gmail.com", "New Contact Request", objContact.Comments, true);
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ContactsDataService.CloseSession();
            }

            return objContact;
        }


        public contact UpdateContact(int contactID, string firstName, string lastName, string email, string address, string homePhone, string officePhone, string street, string city,
            string zipCode, string state, string country, string organization, string designation, string photo,Nullable<bool> allowNewsLetter, string Comment,
            out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            

            contact objContact = new contact();

            try
            {

                ContactsDataService.CreateSession();

                objContact = ContactsDataService.GetContact(contactID);

                objContact.FirstName = WebUtils.UppercaseFirstLetter(firstName.Trim());
                objContact.LastName = WebUtils.UppercaseFirstLetter(lastName.Trim());
                objContact.Email = email.Trim();
                objContact.Address = address.Trim();
                objContact.HomePhone = homePhone.Trim();
                objContact.OfficePhone = officePhone.Trim();
                objContact.Street = street.Trim();
                objContact.City = city.Trim();
                objContact.Zipcode = zipCode.Trim();
                objContact.State = state.Trim();
                objContact.Country = country.Trim();
                objContact.Organization = organization.Trim();
                objContact.Designation = designation.Trim();
                objContact.Photo = photo.Trim();
                objContact.AllowNewsLetter = allowNewsLetter;
                objContact.Comments = Comment.Trim();


                ContactsDataService.BeginTransaction();
                ContactsDataService.UpdateContact(objContact);
                ContactsDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Contact updated successfully.");

            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ContactsDataService.CloseSession();
            }

            return objContact;
        }


        public contact GetContact(int ID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();


            contact objContact = new contact();

            try
            {
                ContactsDataService.CreateSession();
                objContact = ContactsDataService.GetContact(ID);

                if (objContact != null)
                {
                    transaction.ReturnStatus = true;
                }
                else
                {
                    transaction.ReturnStatus = false;
                    transaction.ReturnMessage.Add("Contact id not found.");
                }
            }
            catch (Exception ex)
            {
                WebUtils.TransactionException(transaction, ex);
            }
            finally
            {
                ContactsDataService.CloseSession();
            }

            return objContact;
        }


        public List<contact> ContactInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<contact> contactList = new List<contact>();

            try
            {
                ContactsDataService.CreateSession();
                contactList = ContactsDataService.ContactInquiry(firstName, lastName, paging, out transaction);
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
                ContactsDataService.CloseSession();
            }

            return contactList;
        }
    }
}
