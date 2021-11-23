using EmailSenderAPIS.CommonLayer.Model;
using EmailSenderAPIS.RepositoryLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailSenderAPIS.ServiceLayer
{
    public class EmailSenderSL : IEmailSenderSL
    {
        public readonly IEmailSenderRL _emailSenderRL;
        public readonly ILogger<EmailSenderSL> _logger;

        string EmailStrRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public EmailSenderSL(IEmailSenderRL emailSenderRL, ILogger<EmailSenderSL> logger)
        {
            _emailSenderRL = emailSenderRL;
            _logger = logger;
        }

        public SenderEmailResponse CheckEmailValidation(SenderEmailRequest request)
        {
            Regex regexString = new Regex(EmailStrRegex);
            SenderEmailResponse response = new SenderEmailResponse();
            try
            {
                if (request.EmailId.Trim().Length > 0)
                {
                    if (!regexString.IsMatch(request.EmailId))
                    {
                        response.IsSuccess = false;
                        response.Message = "Email Id Not in Proper Format Ex. Vishal@gmail.com";
                    }

                    response = _emailSenderRL.CheckEmailValidation(request);

                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Email Id Cannot be null";
                    _logger.LogError("Email Id Cannot be null");
                }


            }
            catch(Exception ex)
            {
                _logger.LogError("Error Occur At Service Layer, Message : " + ex.Message);
                response.Message = "Error Occur At Service Layer, Message : " + ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
