using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;

namespace Portal.Application.Services
{
    public class MarkCodeService : IMarkCode
    {
        private readonly IMarkCodeDomain markCodeDomain;

        public MarkCodeService(IMarkCodeDomain markCodeDomain)
        {
            this.markCodeDomain = markCodeDomain;
        }

        public async Task<List<MarkCode>> AddAsync(MarkCodeDTO markCodeDTO)
        {
            return await markCodeDomain.AddAsync(markCodeDTO);
        }
    }
}
