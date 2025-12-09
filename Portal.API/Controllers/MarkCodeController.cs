using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkCodeController : ControllerBase
    {
        private readonly IMarkCode markCodeInterface;

        public MarkCodeController(IMarkCode markCodeInterface)
        {
            this.markCodeInterface = markCodeInterface;
        }

        [HttpPost]
        public async Task<IActionResult> Add(MarkCodeDTO request)
        {
            var result = await markCodeInterface.AddAsync(request);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
