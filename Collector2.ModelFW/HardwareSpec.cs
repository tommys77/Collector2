using System.ComponentModel.DataAnnotations;

namespace Collector2.ModelFW
{
    public class HardwareSpec
    {
        [Key]
        public int HardwareSpecId { get; set; }
        public string HardwareSpecName { get; set; }
        public string BasicSpecs { get; set; }
    }
}