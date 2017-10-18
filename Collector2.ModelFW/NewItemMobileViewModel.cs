using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class NewItemMobileViewModel
    {
        public int ItemId { get; set; }
        public int OwnerId { get; set; }
        public byte[] ImageData { get; set; }
        public string Description { get; set; }
    }
}
