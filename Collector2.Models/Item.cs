using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public List<ItemImage> ItemImages { get; set; }
    }
}
