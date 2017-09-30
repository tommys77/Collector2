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
    public class HardwaresController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/Hardwares
        public IQueryable<Hardware> GetHardware()
        {
            return db.Hardware;
        }

        // GET: api/Hardwares/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult GetHardware(int id)
        {
            Hardware hardware = db.Hardware.Find(id);
            if (hardware == null)
            {
                return NotFound();
            }

            return Ok(hardware);
        }

        // PUT: api/Hardwares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHardware(int id, Hardware hardware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hardware.ItemId)
            {
                return BadRequest();
            }

            db.Entry(hardware).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardwareExists(id))
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

        // POST: api/Hardwares
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult PostHardware(Hardware hardware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hardware.Add(hardware);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HardwareExists(hardware.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hardware.ItemId }, hardware);
        }

        // DELETE: api/Hardwares/5
        [ResponseType(typeof(Hardware))]
        public IHttpActionResult DeleteHardware(int id)
        {
            Hardware hardware = db.Hardware.Find(id);
            if (hardware == null)
            {
                return NotFound();
            }

            db.Hardware.Remove(hardware);
            db.SaveChanges();

            return Ok(hardware);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HardwareExists(int id)
        {
            return db.Hardware.Count(e => e.ItemId == id) > 0;
        }
    }
}