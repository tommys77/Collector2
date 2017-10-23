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
using Collector2.ModelFW;

namespace Collector2.DataService.Controllers
{
    public class ItemsController : ApiController
    {
        private CollectorContext db = new CollectorContext();

        // GET: api/Items
        public IQueryable<Item> GetItem()
        {
            return db.Item;
        }

        // GET: api/UndefinedItems
        [Route("api/UndefinedItems")]
        public IQueryable<UndefinedItem> GetUndefinedItem()
        {
            var query = from i in db.Item
                        join img in db.ItemImage on i.ItemImageId equals img.ItemImageId
                        where i.IsDefined == false
                        select new UndefinedItem()
                        {
                            ItemId = i.ItemId,
                            ItemImageId = i.ItemImageId,
                            ItemDescription = i.ItemDescription,
                            ImageBase64 = img.ImageBase64
                        };

            return query;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ItemId)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(NewItemMobile))]
        public IHttpActionResult PostItem(NewItemMobile newItemMobile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingImage = db.ItemImage.FirstOrDefault(i => i.ImageBase64 == newItemMobile.ImageBase64);

            if (existingImage != null)
            {
                return Conflict();
            }

            var itemImage = new ItemImage()
            {
                ImageBase64 = newItemMobile.ImageBase64
            };

            if (newItemMobile.OwnerId == 99 && db.Owner.Find(99) == null)
            {
                var johnDoe = new Owner
                {
                    OwnerId = 99,
                    FirstName = "John",
                    LastName = "Doe"
                };
                db.Owner.Add(johnDoe);
            }

            var item = new Item()
            {
                ItemDescription = newItemMobile.Description,
                OwnerId = newItemMobile.OwnerId,
                ItemImageId = itemImage.ItemImageId,
                IsDefined = false
            };

            db.ItemImage.Add(itemImage);
            db.Item.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Item.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Item.Count(e => e.ItemId == id) > 0;
        }
    }
}