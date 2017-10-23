using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class UndefinedItem
    {
        public int ItemId { get; set; }
        public int ItemImageId { get; set; }
        public string ItemDescription { get; set; }
        public string ImageBase64 { get; set; }


        public override string ToString()
        {
            return ItemId + ", " +ItemImageId+ ", " + ItemDescription + ", " + ImageBase64;
        }
    }
  

  
}
