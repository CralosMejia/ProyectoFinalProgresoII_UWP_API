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
using AgendaPlusAPI.Models;

namespace AgendaPlusAPI.Controllers
{
    public class ContactosController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/Contactos
        public IQueryable<Contactos> GetContactos()
        {
            return db.Contactos;
        }

        // GET: api/Contactos/5
        [ResponseType(typeof(Contactos))]
        public IHttpActionResult GetContactos(int id)
        {
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return NotFound();
            }

            return Ok(contactos);
        }

        // PUT: api/Contactos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactos(int id, Contactos contactos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactos.ContactoID)
            {
                return BadRequest();
            }

            db.Entry(contactos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactosExists(id))
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

        // POST: api/Contactos
        [ResponseType(typeof(Contactos))]
        public IHttpActionResult PostContactos(Contactos contactos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contactos.Add(contactos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactos.ContactoID }, contactos);
        }

        // DELETE: api/Contactos/5
        [ResponseType(typeof(Contactos))]
        public IHttpActionResult DeleteContactos(int id)
        {
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return NotFound();
            }

            db.Contactos.Remove(contactos);
            db.SaveChanges();

            return Ok(contactos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactosExists(int id)
        {
            return db.Contactos.Count(e => e.ContactoID == id) > 0;
        }
    }
}