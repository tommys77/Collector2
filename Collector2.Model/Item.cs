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
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        [Required, ForeignKey("Owner")]
        public int OwnerId { get; set; }
        [Required, ForeignKey("ItemImage")]
        public int ItemImageId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ItemImage ItemImage { get; set; }
    }
}
