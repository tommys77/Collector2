using System.ComponentModel.DataAnnotations;

namespace Collector2.Model
{
    public class HardwareSpec
    {
        public int HardwareSpecId { get; set; }
        public string HardwareSpecName { get; set; }
        public string BasicSpecs { get; set; }
    }
}