using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class ItemImage
    {
        public int ItemImageId { get; set; }
        public byte[] Image { get; set; }
    }
}
