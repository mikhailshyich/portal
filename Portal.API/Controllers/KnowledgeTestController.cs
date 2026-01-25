using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeTestController : ControllerBase
    {
        private readonly IKnowledgeTestService _knowledgeTestService;

        public KnowledgeTestController(IKnowledgeTestService knowledgeTestService)
        {
            _knowledgeTestService = knowledgeTestService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTest([FromBody] KnowledgeTestDTO request)
        {
            var result = await _knowledgeTestService.AddTestAsync(request);
            if (result.Flag is false) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("question")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddQuestion([FromBody] List<TestQuestionDTO> request)
        {
            var result = await _knowledgeTestService.AddQuestionAsync(request);
            if (result.Flag is false) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("answer")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddQuestionAnswer([FromBody] List<QuestionAnswerDTO> request)
        {
            var result = await _knowledgeTestService.AddQuestionAnswerAsync(request);
            if (result.Flag is false) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("test")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllTests()
        {
            var result = await _knowledgeTestService.GetAllTest();
            if (result.Flag is false) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetTestById(Guid id)
        {
            var result = await _knowledgeTestService.GetTestById(id);
            if (result.Flag is false) return BadRequest(result);
            return Ok(result);
        }
    }
}
