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
using Collector2.Models;

namespace Collector2.DataService.Controllers
{
    public class HardwareSpecsController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/HardwareSpecs
        public IQueryable<HardwareSpec> GetHardwareSpec()
        {
            return db.HardwareSpec;
        }

        // GET: api/HardwareSpecs/5
        [ResponseType(typeof(HardwareSpec))]
        public IHttpActionResult GetHardwareSpec(int id)
        {
            HardwareSpec hardwareSpec = db.HardwareSpec.Find(id);
            if (hardwareSpec == null)
            {
                return NotFound();
            }

            return Ok(hardwareSpec);
        }

        // PUT: api/HardwareSpecs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHardwareSpec(int id, HardwareSpec hardwareSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hardwareSpec.HardwareSpecId)
            {
                return BadRequest();
            }

            db.Entry(hardwareSpec).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardwareSpecExists(id))
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

        // POST: api/HardwareSpecs
        [ResponseType(typeof(HardwareSpec))]
        public IHttpActionResult PostHardwareSpec(HardwareSpec hardwareSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HardwareSpec.Add(hardwareSpec);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hardwareSpec.HardwareSpecId }, hardwareSpec);
        }

        // DELETE: api/HardwareSpecs/5
        [ResponseType(typeof(HardwareSpec))]
        public IHttpActionResult DeleteHardwareSpec(int id)
        {
            HardwareSpec hardwareSpec = db.HardwareSpec.Find(id);
            if (hardwareSpec == null)
            {
                return NotFound();
            }

            db.HardwareSpec.Remove(hardwareSpec);
            db.SaveChanges();

            return Ok(hardwareSpec);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HardwareSpecExists(int id)
        {
            return db.HardwareSpec.Count(e => e.HardwareSpecId == id) > 0;
        }
    }
}