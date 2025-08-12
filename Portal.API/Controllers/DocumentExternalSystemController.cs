using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentExternalSystemController : ControllerBase
    {
        private readonly IDocumentExternalSystem documentExternalSystem;

        public DocumentExternalSystemController(IDocumentExternalSystem documentExternalSystem)
        {
            this.documentExternalSystem = documentExternalSystem;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(DocumentExternalSystemDTO request)
        {
            var result = await documentExternalSystem.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await documentExternalSystem.GetAllAsync();
            return Ok(result);
        }
    }
}
