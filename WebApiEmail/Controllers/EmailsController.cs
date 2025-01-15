using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WebApiEmail.Services;

namespace WebApiEmail.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ILogger<EmailsController> _logger;
        private readonly ISendEmail _sendEmail;

        public EmailsController(ILogger<EmailsController> logger,
                                ISendEmail sendEmail)
        {
            _logger = logger;
            _sendEmail = sendEmail;
        }

        [HttpGet]
        [Route("sendemail")]
        public async Task<IActionResult> TestEmail(string email, string subject, string body)
        {
            try
            {
                await _sendEmail.SendEmailAsync(email, subject, body);
                _logger.LogInformation("Email enviado com sucesso. StatusCode: {StatusCode}", StatusCodes.Status200OK);
                return Ok("Email enviado com sucesso !!!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Erro ao enviar o email");
            }
        }
    }
}
