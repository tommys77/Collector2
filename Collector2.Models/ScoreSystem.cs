using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Models
{
    public class ScoreSystem
    {
        [Key]
        public int ScoreSystemId { get; set; }
        public string ScoreSystemName { get; set; }
    }
}
