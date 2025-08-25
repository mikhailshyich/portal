using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;

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

        [HttpPost("generateqr")]
        public async Task<string> GenerateQR(List<Guid>? listId)
        {
            return await hardwareInterface.GenerateQR(listId);
        }

        [HttpPost("generatelabel")]
        public async Task<string> GenerateLabel(List<Guid>? listId)
        {
            return await hardwareInterface.GenerateLabel(listId);
        }

        [HttpGet("getlabel/{fileName}")]
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

        [HttpPost("move/{userID}/{userWarehouseID}")]
        public async Task<IActionResult> Move(List<Guid> hardwaresID, Guid userID, Guid userWarehouseID)
        {
            var result = await hardwareInterface.MoveToUserAsync(hardwaresID, userID, userWarehouseID);
            return Ok(result);
        }
    }
}
