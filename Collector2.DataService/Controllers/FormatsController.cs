using Collector2.DataContext;
using Collector2.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Collector2.DataService.Controllers
{
    public class FormatsController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/Formats
        public IQueryable<Format> GetFormat()
        {
            return db.Format;
        }

        // GET: api/Formats/5
        [ResponseType(typeof(Format))]
        public IHttpActionResult GetFormat(int id)
        {
            Format format = db.Format.Find(id);
            if (format == null)
            {
                return NotFound();
            }

            return Ok(format);
        }

        // PUT: api/Formats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFormat(int id, Format format)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != format.FormatId)
            {
                return BadRequest();
            }

            db.Entry(format).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormatExists(id))
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

        // POST: api/Formats
        [ResponseType(typeof(Format))]
        public IHttpActionResult PostFormat(Format format)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Format.Add(format);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = format.FormatId }, format);
        }

        // DELETE: api/Formats/5
        [ResponseType(typeof(Format))]
        public IHttpActionResult DeleteFormat(int id)
        {
            Format format = db.Format.Find(id);
            if (format == null)
            {
                return NotFound();
            }

            db.Format.Remove(format);
            db.SaveChanges();

            return Ok(format);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FormatExists(int id)
        {
            return db.Format.Count(e => e.FormatId == id) > 0;
        }
    }
}