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
    public class NotasController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/Notas
        public IQueryable<Notas> GetNotas()
        {
            return db.Notas;
        }

        // GET: api/Notas/5
        [ResponseType(typeof(Notas))]
        public IHttpActionResult GetNotas(int id)
        {
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return NotFound();
            }

            return Ok(notas);
        }

        // PUT: api/Notas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotas(int id, Notas notas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notas.NotaID)
            {
                return BadRequest();
            }

            db.Entry(notas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasExists(id))
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

        // POST: api/Notas
        [ResponseType(typeof(Notas))]
        public IHttpActionResult PostNotas(Notas notas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notas.Add(notas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notas.NotaID }, notas);
        }

        // DELETE: api/Notas/5
        [ResponseType(typeof(Notas))]
        public IHttpActionResult DeleteNotas(int id)
        {
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return NotFound();
            }

            db.Notas.Remove(notas);
            db.SaveChanges();

            return Ok(notas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotasExists(int id)
        {
            return db.Notas.Count(e => e.NotaID == id) > 0;
        }
    }
}