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
using Collector2.Models.ViewModels;
using AutoMapper;

namespace Collector2.DataService.Controllers
{
    public class SoftwaresController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/Softwares
        public IQueryable<SoftwareViewModel> GetSoftware()
        {
            var items = db.Item.Include("ItemImages");

            var software = db.Software.Include("Category")
                .Include("HardwareSpec")
                .Include("Format");

            var result = from s in software
                         join i in items on s.ItemId equals i.ItemId
                         select new SoftwareViewModel()
                         {
                             ItemId = s.ItemId,
                             Title = s.Title,
                             YearOfRelease = s.YearOfRelease,
                             Category = s.Category,
                             Condition = s.Condition,
                             Format = s.Format,
                             FormatCount = s.FormatCount,
                             SoftwareType = s.SoftwareType,
                             Requirements = s.Requirements,
                             ItemImages = i.ItemImages,
                             HardwareSpec = s.HardwareSpec,
                         };


            return result;
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

        // POST : api/Softwares?={imgId}
        [HttpPost]
        [ResponseType(typeof(Software))]
        public IHttpActionResult PostSoftwareWithImage(Software software, int imgId)
        {
            var img = db.ItemImage.Find(imgId);
            img.IsAttached = true;
            img.IsMainImage = true;

            var item = new Item
            {
                ItemImages = new List<ItemImage>() { img }
            };

            var exists = db.Software.Select(s => s.Title == software.Title).FirstOrDefault();

            if (exists)
            {
                return Conflict();
            }

            db.Item.Add(item);

            software.ItemId = item.ItemId;

            db.Software.Add(software);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (SoftwareExists(software.ItemId))
                {
                    return Conflict();
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = software.ItemId }, software);
        }

        [HttpPost]
        [Route("api/UpdateSoftware")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateSoftware(Software software)
        {
            
            db.Software.Attach(software);
            db.Entry(software).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch
            {
                if(!SoftwareExists(software.ItemId))
                {
                    return NotFound();
                }
            }
            
            return StatusCode(HttpStatusCode.OK);
        }


        //// POST: api/Softwares
        //[ResponseType(typeof(Software))]
        //[HttpPost]
        //public IHttpActionResult PostSoftware(SoftwareDTO sw)
        //{
        //    var software = sw.Software;
        //    var imgId = sw.ItemImageId;

        //    var img = db.ItemImage.Find(imgId);

        //    img.IsAttached = true;
        //    var item = new Item
        //    {
        //        ItemImages = new List<ItemImage>() { img }
        //    };

        //    db.Item.Add(item);
        //    software.ItemId = item.ItemId;

        //    db.Software.Add(software);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SoftwareExists(software.ItemId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Ok();
        //    //return CreatedAtRoute("DefaultApi", new { id = software.ItemId }, software);
        //}

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