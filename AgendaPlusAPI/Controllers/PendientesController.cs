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
    public class PendientesController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/Pendientes
        public IQueryable<Pendientes> GetPendientes()
        {
            return db.Pendientes;
        }

        // GET: api/Pendientes/5
        [ResponseType(typeof(Pendientes))]
        public IHttpActionResult GetPendientes(int id)
        {
            Pendientes pendientes = db.Pendientes.Find(id);
            if (pendientes == null)
            {
                return NotFound();
            }

            return Ok(pendientes);
        }

        // PUT: api/Pendientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPendientes(int id, Pendientes pendientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pendientes.PendienteID)
            {
                return BadRequest();
            }

            db.Entry(pendientes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendientesExists(id))
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

        // POST: api/Pendientes
        [ResponseType(typeof(Pendientes))]
        public IHttpActionResult PostPendientes(Pendientes pendientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pendientes.Add(pendientes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pendientes.PendienteID }, pendientes);
        }

        // DELETE: api/Pendientes/5
        [ResponseType(typeof(Pendientes))]
        public IHttpActionResult DeletePendientes(int id)
        {
            Pendientes pendientes = db.Pendientes.Find(id);
            if (pendientes == null)
            {
                return NotFound();
            }

            db.Pendientes.Remove(pendientes);
            db.SaveChanges();

            return Ok(pendientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PendientesExists(int id)
        {
            return db.Pendientes.Count(e => e.PendienteID == id) > 0;
        }
    }
}