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
        [Required, ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public string ImageString { get; set; }

        public virtual Owner Owner { get; set; }
    }
}
