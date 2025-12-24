using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("hardwares/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _historyService.GetByHardwareIdAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }
    }
}
