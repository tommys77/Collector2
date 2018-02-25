using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Item> Items { get; set; }
    }
}
