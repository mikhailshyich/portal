using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(HardwareDTO request)
        {
            var result = await hardwareInterface.AddAsync(request);
            return CreatedAtRoute("", result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user,employee_it")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Move([FromBody] HardwareMoveDTO moveDTO)
        {
            var result = await hardwareInterface.MoveToUserAsync(moveDTO);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await hardwareInterface.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPatch("return")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Return([FromBody] HardwareReturnDTO returnDTO)
        {
            var result = await hardwareInterface.ReturnAsync(returnDTO);
            if (result.Flag is false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("import")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Import(List<HardwareImportDTO> hardwares)
        {
            var result = await hardwareInterface.Import(hardwares);
            return Ok(result);
        }

        [HttpGet("export")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> Export()
        {
            var result = await hardwareInterface.Export();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            DateTime now = DateTime.Now;
            string dateString = now.ToString("dd.MM.yyyy");
            return File(result, contentType, $"Оборудование ({dateString}).xlsx");
        }

        [HttpPost("mark")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            var result = await hardwareInterface.MarkHardware(markHardwareDTO);
            return Ok(result);
        }

        [HttpPatch("marking")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> MarkHardware([FromBody]MarkAllHardwareDTO hardwareDTO)
        {
            var result = await hardwareInterface.MarkAllHardware(hardwareDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await hardwareInterface.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] HardwareUpdateDTO updateDTO)
        {
            var result = await hardwareInterface.UpdateAsync(updateDTO);
            return Ok(result);
        }

        [HttpPatch("writeoff")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> WriteOff([FromBody] HardwareWriteOffDTO writeOffDTO)
        {
            var result = await hardwareInterface.WriteOff(writeOffDTO);
            return Ok(result);
        }
    }
}
