using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class NewItemMobile
    {
        public int ItemId { get; set; }
        public int OwnerId { get; set; }
        public string ImageBase64 { get; set; }
        public string Description { get; set; }
    }
}
