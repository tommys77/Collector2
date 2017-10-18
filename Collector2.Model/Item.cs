using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public int OwnerId { get; set; }
        public int ItemImageId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ItemImage ItemImage { get; set; }
    }
}
