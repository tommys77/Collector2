using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Models
{
    public class Software
    {
        [Key]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public string SoftwareType { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int FormatCount { get; set; }
        public string Requirements { get; set; }
        public string Condition { get; set; }

        [ForeignKey("Format")]
        public int FormatId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("HardwareSpec")]
        public int HardwareSpecId { get; set; }

        public virtual Item Item { get; set; }
        public virtual HardwareSpec HardwareSpec { get; set; }
        public virtual Category Category { get; set; }
        public virtual Format Format { get; set; }
    }
}
