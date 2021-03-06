﻿using System;
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
    public class ItemImagesController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/ItemImages
        public IQueryable<ItemImage> GetItemImage()
        {
            return db.ItemImage;
        }

        // GET: api/ItemImages/5
        [ResponseType(typeof(ItemImage))]
        public IHttpActionResult GetItemImage(int id)
        {
            ItemImage itemImage = db.ItemImage.Find(id);
            if (itemImage == null)
            {
                return NotFound();
            }

            return Ok(itemImage);
        }

        [HttpPost]
        [Route("api/AttachOrDetachImage")]
        public IHttpActionResult AttachOrDetachImageToItem(int imgId, int itemId)
        {
            var img = db.ItemImage.Find(imgId);
            var item = db.Item.Find(itemId);

            if (img == null || item == null)
            {
                return NotFound();
            }

            if (item.ItemImages != null && item.ItemImages.Where(image => image.ItemImageId == img.ItemImageId).FirstOrDefault() != null)
            {
                item.ItemImages.Remove(img);
            }

            else
            {
                item.ItemImages = new List<ItemImage>();
                item.ItemImages.Add(img);
                img.IsAttached = true;
            }

            db.SaveChanges();

            return Ok();
        }

        // PUT: api/ItemImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItemImage(int id, ItemImage itemImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemImage.ItemImageId)
            {
                return BadRequest();
            }

            db.Entry(itemImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemImageExists(id))
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

        // GET: api/UnattachedItems
        [HttpGet]
        [Route("api/UnattachedImages")]
        public IQueryable<ItemImage> GetUnattachedImages()
        {
            var query = from i in db.ItemImage
                        where i.IsAttached == false
                        select i;

            return query;
        }

        // GET: api/UnattachedItemsExists
        [HttpGet]
        [Route("api/UnattachedImagesExists")]
        public IHttpActionResult UnattachedImagesExists()
        {
            var img = db.ItemImage.Where(i => i.IsAttached == false).FirstOrDefault();

            if (img == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/ItemImages
        [HttpPost]
        [ResponseType(typeof(ItemImage))]
        public IHttpActionResult PostItemImage(ItemImage itemImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingImage = db.ItemImage.FirstOrDefault(i => i.ImageBase64 == itemImage.ImageBase64);
            if (existingImage != null)
            {
                return Conflict();
            }

            db.ItemImage.Add(itemImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = itemImage.ItemImageId }, itemImage);
        }

        // DELETE: api/ItemImages/5
        [ResponseType(typeof(ItemImage))]
        public IHttpActionResult DeleteItemImage(int id)
        {
            ItemImage itemImage = db.ItemImage.Find(id);
            if (itemImage == null)
            {
                return NotFound();
            }

            db.ItemImage.Remove(itemImage);
            db.SaveChanges();

            return Ok(itemImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemImageExists(int id)
        {
            return db.ItemImage.Count((System.Linq.Expressions.Expression<Func<ItemImage, bool>>)(e => e.ItemImageId == id)) > 0;
        }
    }
}