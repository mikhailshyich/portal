using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class MarkCodeRepository : IMarkCodeDomain
    {
        private readonly PortalDbContext context;

        public MarkCodeRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<MarkCode>> AddAsync(MarkCodeDTO markCodeDTO)
        {
            if (markCodeDTO is null) return null!;

            var markCodeList = new List<MarkCode>();

            if(markCodeDTO.Count > 0)
            {
                for(int i = 0; i < markCodeDTO.Count; i++)
                {
                    var markCode = new MarkCode();
                    markCodeList.Add(markCode);
                }
                context.AddRange(markCodeList);
                await context.SaveChangesAsync();
            }

            return markCodeList;
        }
    }
}
