using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Models
{
    public class ItemImage
    {
        [Key]
        public int ItemImageId { get; set; }
        public string ImageBase64 { get; set; }
        public string Description { get; set; }
        public bool IsAttached { get; set; } = false;
    }
}
