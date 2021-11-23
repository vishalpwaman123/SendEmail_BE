using EmailSenderAPIS.CommonLayer.Model;
using EmailSenderAPIS.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {

        public readonly IEmailSenderSL _emailSenderSL;
        public readonly ILogger<EmailSenderController> _logger;

        public EmailSenderController(IEmailSenderSL emailSenderSL, ILogger<EmailSenderController> logger)
        {
            _emailSenderSL = emailSenderSL;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CheckValidEmailID(SenderEmailRequest request)
        {
            SenderEmailResponse response = null;
            try
            {

                response = _emailSenderSL.CheckEmailValidation(request);
                if (response.IsSuccess == false)
                {
                    return BadRequest(response);
                }

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error Occur At Controller, Message : " + ex.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
