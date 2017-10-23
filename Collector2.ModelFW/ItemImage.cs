using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.ModelFW
{
    public class ItemImage
    {
        [Key]
        public int ItemImageId { get; set; }
        public string ImageBase64 { get; set; }
    }
}
