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
    public class ScoreSystemsController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/ScoreSystems
        public IQueryable<ScoreSystem> GetScoreSystem()
        {
            return db.ScoreSystem;
        }

        // GET: api/ScoreSystems/5
        [ResponseType(typeof(ScoreSystem))]
        public IHttpActionResult GetScoreSystem(int id)
        {
            ScoreSystem scoreSystem = db.ScoreSystem.Find(id);
            if (scoreSystem == null)
            {
                return NotFound();
            }

            return Ok(scoreSystem);
        }

        // PUT: api/ScoreSystems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScoreSystem(int id, ScoreSystem scoreSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scoreSystem.ScoreSystemId)
            {
                return BadRequest();
            }

            db.Entry(scoreSystem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreSystemExists(id))
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

        // POST: api/ScoreSystems
        [ResponseType(typeof(ScoreSystem))]
        public IHttpActionResult PostScoreSystem(ScoreSystem scoreSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScoreSystem.Add(scoreSystem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = scoreSystem.ScoreSystemId }, scoreSystem);
        }

        // DELETE: api/ScoreSystems/5
        [ResponseType(typeof(ScoreSystem))]
        public IHttpActionResult DeleteScoreSystem(int id)
        {
            ScoreSystem scoreSystem = db.ScoreSystem.Find(id);
            if (scoreSystem == null)
            {
                return NotFound();
            }

            db.ScoreSystem.Remove(scoreSystem);
            db.SaveChanges();

            return Ok(scoreSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScoreSystemExists(int id)
        {
            return db.ScoreSystem.Count(e => e.ScoreSystemId == id) > 0;
        }
    }
}