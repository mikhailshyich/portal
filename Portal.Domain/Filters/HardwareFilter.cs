using Portal.Domain.Entities.Hardwares;

namespace Portal.Domain.Filters
{
    public class HardwareFilter
    {
        public string? StatusTitle { get; set; }
        public CategoryHardware? CategoryHardware { get; set; }
    }
}
