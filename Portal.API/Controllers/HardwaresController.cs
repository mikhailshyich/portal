using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwaresController : ControllerBase
    {
        private readonly IHardware hardwareInterface;

        public HardwaresController(IHardware hardwareInterface)
        {
            this.hardwareInterface = hardwareInterface;
        }

        [HttpPost]
        public async Task<IActionResult> Add(HardwareDTO request)
        {
            var result = await hardwareInterface.AddAsync(request);
            return CreatedAtRoute("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await hardwareInterface.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("qr")]
        public async Task<string> GenerateQR(List<Guid>? listId)
        {
            return await hardwareInterface.GenerateQR(listId);
        }

        [HttpPost("label")]
        public async Task<string> GenerateLabel(List<Guid>? listId)
        {
            return await hardwareInterface.GenerateLabel(listId);
        }

        [HttpGet("label/{fileName}")]
        public async Task<IActionResult> File(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "labels", fileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType);
                //return File(bytes, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPatch("move")]
        public async Task<IActionResult> Move([FromBody] HardwareMoveDTO moveDTO)
        {
            var result = await hardwareInterface.MoveToUserAsync(moveDTO);
            return Ok(result);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await hardwareInterface.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPatch("return")]
        public async Task<IActionResult> Return([FromBody] HardwareReturnDTO returnDTO)
        {
            var result = await hardwareInterface.ReturnAsync(returnDTO);
            if (result.Flag is false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(List<HardwareImportDTO> hardwares)
        {
            var result = await hardwareInterface.Import(hardwares);
            return Ok(result);
        }

        [HttpPost("mark")]
        public async Task<IActionResult> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            var result = await hardwareInterface.MarkHardware(markHardwareDTO);
            return Ok(result);
        }

        [HttpPatch("marking")]
        public async Task<IActionResult> MarkHardware([FromBody]List<Guid> hardwareId)
        {
            var result = await hardwareInterface.MarkAllHardware(hardwareId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await hardwareInterface.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
