using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RpaWebApi.Models;
using RpaWebApi.Services;

namespace RpaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RpaController : ControllerBase
    {
        private readonly RpaService _rpaService;

        public RpaController(RpaService rpaService)
        {
            _rpaService = rpaService;
        }

        [HttpPost("download")]
        public async Task<IActionResult> DownloadFile([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.WebsiteUrl) || string.IsNullOrEmpty(request.DownloadUrl))
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                var fileBytes = await _rpaService.LoginAndDownloadFileAsync(request);
                return File(fileBytes, "application/pdf", "downloaded_file.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
