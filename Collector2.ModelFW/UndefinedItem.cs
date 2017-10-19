using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.ModelFW
{
    public class UndefinedItem
    {
        public int ItemId { get; set; }
        public int ItemImageId { get; set; }
        public string ItemDescription { get; set; }
        public byte[] Image { get; set; }
    }
}
