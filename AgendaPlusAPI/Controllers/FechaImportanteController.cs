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
    public class FechaImportanteController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/FechaImportante
        public IQueryable<FechasImportante> GetFechasImportantes()
        {
            return db.FechasImportantes;
        }

        // GET: api/FechaImportante/5
        [ResponseType(typeof(FechasImportante))]
        public IHttpActionResult GetFechasImportante(int id)
        {
            FechasImportante fechasImportante = db.FechasImportantes.Find(id);
            if (fechasImportante == null)
            {
                return NotFound();
            }

            return Ok(fechasImportante);
        }

        // PUT: api/FechaImportante/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFechasImportante(int id, FechasImportante fechasImportante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fechasImportante.FechasImportantesID)
            {
                return BadRequest();
            }

            db.Entry(fechasImportante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechasImportanteExists(id))
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

        // POST: api/FechaImportante
        [ResponseType(typeof(FechasImportante))]
        public IHttpActionResult PostFechasImportante(FechasImportante fechasImportante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FechasImportantes.Add(fechasImportante);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fechasImportante.FechasImportantesID }, fechasImportante);
        }

        // DELETE: api/FechaImportante/5
        [ResponseType(typeof(FechasImportante))]
        public IHttpActionResult DeleteFechasImportante(int id)
        {
            FechasImportante fechasImportante = db.FechasImportantes.Find(id);
            if (fechasImportante == null)
            {
                return NotFound();
            }

            db.FechasImportantes.Remove(fechasImportante);
            db.SaveChanges();

            return Ok(fechasImportante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechasImportanteExists(int id)
        {
            return db.FechasImportantes.Count(e => e.FechasImportantesID == id) > 0;
        }
    }
}