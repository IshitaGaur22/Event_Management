
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public TestEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("SendTest")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                await _emailService.SendEmailAsync("ummitibhavyasri@gmail.com", "Test Email", "This is a test email.");
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Email failed: " + ex.Message);
            }
        }
    }
}
