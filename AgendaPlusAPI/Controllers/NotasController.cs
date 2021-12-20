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
        public IQueryable<Nota> GetNotas()
        {
            return db.Notas;
        }

        // GET: api/Notas/5
        [ResponseType(typeof(Nota))]
        public IHttpActionResult GetNota(int id)
        {
            Nota nota = db.Notas.Find(id);
            if (nota == null)
            {
                return NotFound();
            }

            return Ok(nota);
        }

        // PUT: api/Notas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNota(int id, Nota nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nota.NotaID)
            {
                return BadRequest();
            }

            db.Entry(nota).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(id))
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
        [ResponseType(typeof(Nota))]
        public IHttpActionResult PostNota(Nota nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notas.Add(nota);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nota.NotaID }, nota);
        }

        // DELETE: api/Notas/5
        [ResponseType(typeof(Nota))]
        public IHttpActionResult DeleteNota(int id)
        {
            Nota nota = db.Notas.Find(id);
            if (nota == null)
            {
                return NotFound();
            }

            db.Notas.Remove(nota);
            db.SaveChanges();

            return Ok(nota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotaExists(int id)
        {
            return db.Notas.Count(e => e.NotaID == id) > 0;
        }
    }
}