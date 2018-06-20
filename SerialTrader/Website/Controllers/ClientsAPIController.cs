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
    public class ClientsAPIController : ApiController
    {
        private waterconsEntities db = new waterconsEntities();

        // GET: api/ClientsAPI
        public IQueryable<client> Getclients()
        {
            return db.clients;
        }

        // GET: api/ClientsAPI/5
        [ResponseType(typeof(client))]
        public IHttpActionResult Getclient(int id)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/ClientsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putclient(int id, client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientExists(id))
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

        // POST: api/ClientsAPI
        [ResponseType(typeof(client))]
        public IHttpActionResult Postclient(client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
        }

        // DELETE: api/ClientsAPI/5
        [ResponseType(typeof(client))]
        public IHttpActionResult Deleteclient(int id)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientExists(int id)
        {
            return db.clients.Count(e => e.ID == id) > 0;
        }
    }
}