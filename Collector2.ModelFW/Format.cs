using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class Format
    {
        [Key]
        public int FormatId { get; set; }
        public string FormatName { get; set; }
    }
}
