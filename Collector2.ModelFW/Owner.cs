using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.ModelFW
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
