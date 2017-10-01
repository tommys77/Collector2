using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Model
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Score { get; set; }
        public int ScorePercentage { get; set; }
        [ForeignKey("ScoreSystem")]
        public int ScoreSystemId { get; set; }
        [ForeignKey("Software")]
        public int ItemId { get; set; }

        public virtual ScoreSystem ScoreSystem { get; set; }
        public virtual Software Software { get; set; }


    }
}
