using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

//using Website.Helpers;
using Website.Filters;

using AppLibrary.Interfaces;
using AppLibrary.DataServices;
using AppLibrary.Business;
using AppLibrary.Entity;
using AppLibrary.Model;
using AppLibrary.Common;


namespace Website.Controllers
{

    [RoutePrefix("api/contacts")]
    public class ContactsAPIController : ApiController
    {
        private serialtraderEntities db = new serialtraderEntities();

        IContactDataService contactDataService;

        public ContactsAPIController()
        {
            contactDataService = new ContactDataService();
        }


        [Route("CreateContact")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage CreateContact(HttpRequestMessage request, [FromBody] ContactInfo objContactInfo)
        {
            
            TransactionalInformation transaction = new TransactionalInformation();
            ContactBusinessService contactBusinessService;

            if ((string.IsNullOrEmpty(objContactInfo.OfficePhone)) && (string.IsNullOrEmpty(objContactInfo.Email)))
            {
                objContactInfo.ReturnStatus = false;
                objContactInfo.ReturnMessage.Add("Please leave any of your contact information");
                var badResponse = Request.CreateResponse<ContactInfo>(HttpStatusCode.BadRequest, objContactInfo);
                return badResponse;
            }

            if (objContactInfo.FullName != null)
            {
                if (objContactInfo.FullName.Contains(","))
                {
                    string[] Names = objContactInfo.FullName.Split(',');
                    objContactInfo.FirstName = Names[1].ToString();
                    objContactInfo.LastName = Names[0].ToString();
                }
                else
                {
                    objContactInfo.FirstName = "";
                    objContactInfo.LastName = objContactInfo.FullName;
                }
            }
            if (objContactInfo.AllowNewsLetter == null) objContactInfo.AllowNewsLetter = false;
          
            contactBusinessService = new ContactBusinessService(contactDataService);
            contact objContact = contactBusinessService.AddContact(
                objContactInfo.FirstName,
                objContactInfo.LastName,
                objContactInfo.Email,
                objContactInfo.Address,
                objContactInfo.HomePhone,
                objContactInfo.OfficePhone,
                objContactInfo.Street,
                objContactInfo.City,
                objContactInfo.ZipCode,
                objContactInfo.State,
                objContactInfo.Country,
                objContactInfo.Organization,
                objContactInfo.Designation,
                objContactInfo.Photo,
                objContactInfo.AllowNewsLetter,
                objContactInfo.Comments,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objContactInfo.ReturnMessage = transaction.ReturnMessage;
                objContactInfo.ReturnStatus = transaction.ReturnStatus;
                objContactInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<ContactInfo>(HttpStatusCode.BadRequest, objContactInfo);
                return badResponse;
            }

            objContactInfo.IsAuthenicated = true;
            objContactInfo.ReturnStatus = transaction.ReturnStatus;
            objContactInfo.ReturnMessage.Add("Register User successful.");
           
            var response = Request.CreateResponse<ContactInfo>(HttpStatusCode.OK, objContactInfo);
            return response;
        }


        [Route("UpdateContact")]
        [ValidateModelState]
        [HttpPost]
        public HttpResponseMessage UpdateContact(HttpRequestMessage request, [FromBody] ContactInfo objContactInfo)
        {
            TransactionalInformation transaction = new TransactionalInformation();
            ContactBusinessService contactBusinessService;

            if (objContactInfo.FullName != null)
            {
                if (objContactInfo.FullName.Contains(","))
                {
                    string[] Names = objContactInfo.FullName.Split(',');
                    objContactInfo.FullName = Names[1].ToString();
                    objContactInfo.LastName = Names[0].ToString(); ;
                }
                else
                {
                    objContactInfo.FullName = "";
                    objContactInfo.LastName = objContactInfo.FullName;
                }
            }

            if (objContactInfo.AllowNewsLetter == false) objContactInfo.AllowNewsLetter = false;

            contactBusinessService = new ContactBusinessService(contactDataService);
            contact objContact = contactBusinessService.UpdateContact(
                objContactInfo.ID,
                objContactInfo.FirstName,
                objContactInfo.LastName,
                objContactInfo.Email,
                objContactInfo.Address,
                objContactInfo.HomePhone,
                objContactInfo.OfficePhone,
                objContactInfo.Street,
                objContactInfo.City,
                objContactInfo.ZipCode,
                objContactInfo.State,
                objContactInfo.Country,
                objContactInfo.Organization,
                objContactInfo.Designation,
                objContactInfo.Photo,
                objContactInfo.AllowNewsLetter,
                objContactInfo.Comments,
                out transaction);

            if (transaction.ReturnStatus == false)
            {
                objContactInfo.ReturnMessage = transaction.ReturnMessage;
                objContactInfo.ReturnStatus = transaction.ReturnStatus;
                objContactInfo.ValidationErrors = transaction.ValidationErrors;
                var badResponse = Request.CreateResponse<ContactInfo>(HttpStatusCode.BadRequest, objContactInfo);
                return badResponse;
            }

            objContactInfo.ReturnStatus = transaction.ReturnStatus;
            objContactInfo.ReturnMessage = transaction.ReturnMessage;
            
            var response = Request.CreateResponse<ContactInfo>(HttpStatusCode.OK, objContactInfo);
            return response;
        }
        
        [Route("GetContacts")]
        [HttpPost]
        [WebApiAuthenication]
        [ValidateModelState]
        public HttpResponseMessage GetContacts([FromBody] ContactInfo objContactInfo)
        {
            if (objContactInfo.FirstName == null) objContactInfo.FirstName = string.Empty;
            if (objContactInfo.LastName == null) objContactInfo.LastName = string.Empty;
            if (objContactInfo.SortDirection == null) objContactInfo.SortDirection = string.Empty;
            if (objContactInfo.SortExpression == null) objContactInfo.SortExpression = string.Empty;

            TransactionalInformation transaction = new TransactionalInformation();
            ContactBusinessService contactBusinessService;

            objContactInfo.IsAuthenicated = true;

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = objContactInfo.CurrentPageNumber;
            paging.PageSize = objContactInfo.PageSize;
            paging.SortExpression = objContactInfo.SortExpression;
            paging.SortDirection = objContactInfo.SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "DESC";
            if (paging.SortExpression == "") paging.SortExpression = "FirstName";

            contactBusinessService = new ContactBusinessService(contactDataService);

            List<contact> contacts = contactBusinessService.ContactInquiry(objContactInfo.FirstName, objContactInfo.LastName, paging, out transaction);

            objContactInfo.Contacts = contacts;
            objContactInfo.ReturnStatus = transaction.ReturnStatus;
            objContactInfo.ReturnMessage = transaction.ReturnMessage;
            objContactInfo.TotalPages = transaction.TotalPages;
            objContactInfo.TotalRows = transaction.TotalRows;
            objContactInfo.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<ContactInfo>(HttpStatusCode.OK, objContactInfo);
                return response;
            }

            var badResponse = Request.CreateResponse<ContactInfo>(HttpStatusCode.BadRequest, objContactInfo);
            return badResponse;
        }


        // GET: api/ContactsAPI
        public IQueryable<contact> Getcontacts()
        {
            return db.contacts;
        }

        // GET: api/ContactsAPI/5
        [ResponseType(typeof(contact))]
        [Route("GetContact")]
        [HttpGet]
        [WebApiAuthenication]
        [ValidateModelState]
        public IHttpActionResult Getcontact(int id)
        {
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/ContactsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcontact(int id, contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ID)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!contactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContactsAPI
        [ResponseType(typeof(contact))]
        public IHttpActionResult Postcontact(contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.ID }, contact);
        }

        // DELETE: api/ContactsAPI/5
        [ResponseType(typeof(contact))]
        public IHttpActionResult Deletecontact(int id)
        {
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool contactExists(int id)
        {
            return db.contacts.Count(e => e.ID == id) > 0;
        }
    }
}