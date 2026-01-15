using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentExternalSystem documentExternalSystem;

        public DocumentsController(IDocumentExternalSystem documentExternalSystem)
        {
            this.documentExternalSystem = documentExternalSystem;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody]DocumentExternalSystemDTO request)
        {
            var result = await documentExternalSystem.AddAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetAll()
        {
            var result = await documentExternalSystem.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await documentExternalSystem.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
