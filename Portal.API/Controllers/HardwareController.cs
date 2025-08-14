using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardware hardwareInterface;

        public HardwareController(IHardware hardwareInterface)
        {
            this.hardwareInterface = hardwareInterface;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(HardwareDTO request)
        {
            var result = await hardwareInterface.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await hardwareInterface.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("generateqr/{id}")]
        public void GenerateQR(Guid id)
        {
            hardwareInterface.GenerateQR(id, null);
        }

        [HttpPost("generate")]
        public async Task<string> GenerateQR(List<Guid> id)
        {
            return await hardwareInterface.GenerateQR(null, id);
        }

        [HttpGet("file")]
        public async Task<IActionResult> File()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Labels", "ZQ5Bp+wP8vOWDg==-qr.pdf");
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
