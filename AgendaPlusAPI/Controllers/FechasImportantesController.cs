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
    public class FechasImportantesController : ApiController
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: api/FechasImportantes
        public IQueryable<FechasImportantes> GetFechasImportantes()
        {
            return db.FechasImportantes;
        }

        // GET: api/FechasImportantes/5
        [ResponseType(typeof(FechasImportantes))]
        public IHttpActionResult GetFechasImportantes(int id)
        {
            FechasImportantes fechasImportantes = db.FechasImportantes.Find(id);
            if (fechasImportantes == null)
            {
                return NotFound();
            }

            return Ok(fechasImportantes);
        }

        // PUT: api/FechasImportantes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFechasImportantes(int id, FechasImportantes fechasImportantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fechasImportantes.FechasImportantesID)
            {
                return BadRequest();
            }

            db.Entry(fechasImportantes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechasImportantesExists(id))
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

        // POST: api/FechasImportantes
        [ResponseType(typeof(FechasImportantes))]
        public IHttpActionResult PostFechasImportantes(FechasImportantes fechasImportantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FechasImportantes.Add(fechasImportantes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fechasImportantes.FechasImportantesID }, fechasImportantes);
        }

        // DELETE: api/FechasImportantes/5
        [ResponseType(typeof(FechasImportantes))]
        public IHttpActionResult DeleteFechasImportantes(int id)
        {
            FechasImportantes fechasImportantes = db.FechasImportantes.Find(id);
            if (fechasImportantes == null)
            {
                return NotFound();
            }

            db.FechasImportantes.Remove(fechasImportantes);
            db.SaveChanges();

            return Ok(fechasImportantes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechasImportantesExists(int id)
        {
            return db.FechasImportantes.Count(e => e.FechasImportantesID == id) > 0;
        }
    }
}