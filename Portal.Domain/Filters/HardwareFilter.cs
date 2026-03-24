using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;

namespace Portal.Domain.Filters
{
    public class HardwareFilter
    {
        public string? StatusTitle { get; set; }
        public CategoryHardware? CategoryHardware { get; set; }
        public UserView? UserView { get; set; }
    }
}
