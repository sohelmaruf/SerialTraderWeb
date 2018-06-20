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
    public class ListItemsAPIController : ApiController
    {
        private waterconsEntities db = new waterconsEntities();

        // GET: api/ListItemsAPI
        public IQueryable<listitem> Getlistitems()
        {
            return db.listitems;
        }

        // GET: api/ListItemsAPI/5
        [ResponseType(typeof(listitem))]
        public IHttpActionResult Getlistitem(int id)
        {
            listitem listitem = db.listitems.Find(id);
            if (listitem == null)
            {
                return NotFound();
            }

            return Ok(listitem);
        }

        // PUT: api/ListItemsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlistitem(int id, listitem listitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listitem.ID)
            {
                return BadRequest();
            }

            db.Entry(listitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listitemExists(id))
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

        // POST: api/ListItemsAPI
        [ResponseType(typeof(listitem))]
        public IHttpActionResult Postlistitem(listitem listitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.listitems.Add(listitem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = listitem.ID }, listitem);
        }

        // DELETE: api/ListItemsAPI/5
        [ResponseType(typeof(listitem))]
        public IHttpActionResult Deletelistitem(int id)
        {
            listitem listitem = db.listitems.Find(id);
            if (listitem == null)
            {
                return NotFound();
            }

            db.listitems.Remove(listitem);
            db.SaveChanges();

            return Ok(listitem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool listitemExists(int id)
        {
            return db.listitems.Count(e => e.ID == id) > 0;
        }
    }
}