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
using Collector2.DataContext;
using Collector2.Model;

namespace Collector2.DataService.Controllers
{
    public class SoftwaresController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/Softwares
        public IQueryable<Software> GetSoftware()
        {
            return db.Software;
        }

        // GET: api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult GetSoftware(int id)
        {
            Software software = db.Software.Find(id);
            if (software == null)
            {
                return NotFound();
            }

            return Ok(software);
        }

        // PUT: api/Softwares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSoftware(int id, Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != software.ItemId)
            {
                return BadRequest();
            }

            db.Entry(software).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftwareExists(id))
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

        // POST: api/Softwares
        [ResponseType(typeof(Software))]
        public IHttpActionResult PostSoftware(Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Software.Add(software);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SoftwareExists(software.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = software.ItemId }, software);
        }

        // DELETE: api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult DeleteSoftware(int id)
        {
            Software software = db.Software.Find(id);
            if (software == null)
            {
                return NotFound();
            }

            db.Software.Remove(software);
            db.SaveChanges();

            return Ok(software);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SoftwareExists(int id)
        {
            return db.Software.Count(e => e.ItemId == id) > 0;
        }
    }
}