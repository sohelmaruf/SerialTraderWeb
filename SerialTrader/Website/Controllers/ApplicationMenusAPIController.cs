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
using WaterCons.Library.Models;

namespace WaterCons.Controllers
{
    public class ApplicationMenusAPIController : ApiController
    {
        private waterconsEntities db = new waterconsEntities();

        // GET: api/ApplicationmenusAPI
        public IQueryable<applicationmenu> Getapplicationmenus()
        {
            return db.applicationmenus;
        }

        // GET: api/ApplicationmenusAPI/5
        [ResponseType(typeof(applicationmenu))]
        public IHttpActionResult Getapplicationmenu(int id)
        {
            applicationmenu applicationmenu = db.applicationmenus.Find(id);
            if (applicationmenu == null)
            {
                return NotFound();
            }

            return Ok(applicationmenu);
        }

        // PUT: api/ApplicationmenusAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapplicationmenu(int id, applicationmenu applicationmenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationmenu.ID)
            {
                return BadRequest();
            }

            db.Entry(applicationmenu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!applicationmenuExists(id))
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

        // POST: api/ApplicationmenusAPI
        [ResponseType(typeof(applicationmenu))]
        public IHttpActionResult Postapplicationmenu(applicationmenu applicationmenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.applicationmenus.Add(applicationmenu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = applicationmenu.ID }, applicationmenu);
        }

        // DELETE: api/ApplicationmenusAPI/5
        [ResponseType(typeof(applicationmenu))]
        public IHttpActionResult Deleteapplicationmenu(int id)
        {
            applicationmenu applicationmenu = db.applicationmenus.Find(id);
            if (applicationmenu == null)
            {
                return NotFound();
            }

            db.applicationmenus.Remove(applicationmenu);
            db.SaveChanges();

            return Ok(applicationmenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool applicationmenuExists(int id)
        {
            return db.applicationmenus.Count(e => e.ID == id) > 0;
        }
    }
}