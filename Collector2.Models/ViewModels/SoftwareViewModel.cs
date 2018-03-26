using System;
using System.Collections.Generic;
using System.Text;

namespace Collector2.Models.ViewModels
{
    public class SoftwareViewModel
    {
        public int ItemId { get; set; }
        public string SoftwareType { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int FormatCount { get; set; }
        public string Requirements { get; set; }
        public string Condition { get; set; }

        public List<ItemImage> ItemImages { get; set; }
        public virtual HardwareSpec HardwareSpec { get; set; }
        public virtual Category Category { get; set; }
        public virtual Format Format { get; set; }
    }
}
