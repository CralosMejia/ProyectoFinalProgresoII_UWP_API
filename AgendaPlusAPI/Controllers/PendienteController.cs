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
    public class PendienteController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/Pendiente
        public IQueryable<Pendiente> GetPendientes()
        {
            return db.Pendientes;
        }

        // GET: api/Pendiente/5
        [ResponseType(typeof(Pendiente))]
        public IHttpActionResult GetPendiente(int id)
        {
            Pendiente pendiente = db.Pendientes.Find(id);
            if (pendiente == null)
            {
                return NotFound();
            }

            return Ok(pendiente);
        }

        // PUT: api/Pendiente/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPendiente(int id, Pendiente pendiente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pendiente.PendienteID)
            {
                return BadRequest();
            }

            db.Entry(pendiente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendienteExists(id))
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

        // POST: api/Pendiente
        [ResponseType(typeof(Pendiente))]
        public IHttpActionResult PostPendiente(Pendiente pendiente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pendientes.Add(pendiente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pendiente.PendienteID }, pendiente);
        }

        // DELETE: api/Pendiente/5
        [ResponseType(typeof(Pendiente))]
        public IHttpActionResult DeletePendiente(int id)
        {
            Pendiente pendiente = db.Pendientes.Find(id);
            if (pendiente == null)
            {
                return NotFound();
            }

            db.Pendientes.Remove(pendiente);
            db.SaveChanges();

            return Ok(pendiente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PendienteExists(int id)
        {
            return db.Pendientes.Count(e => e.PendienteID == id) > 0;
        }
    }
}