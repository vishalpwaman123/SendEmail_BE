using EmailSenderAPIS.CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderAPIS.RepositoryLayer
{
    public class EmailSenderRL : IEmailSenderRL
    {
        public readonly ILogger<EmailSenderRL> __logger;
        public readonly IConfiguration __configuration;

        public EmailSenderRL(ILogger<EmailSenderRL> logger, IConfiguration configuration)
        {
            __logger = logger;
            __configuration = configuration;
        }

        public SenderEmailResponse CheckEmailValidation(SenderEmailRequest request)
        {
            SenderEmailResponse response = new SenderEmailResponse();
            InternetConnectionResponse response1 = null;
            response.IsSuccess = true;
            response.Message = "Email Send SuccessFully";

            try
            {

                // Check Internet Connection
                response1 = IsConnectedToInternet();
                if (response1.IsSuccess == false)
                {
                    response.Message = response1.Message;
                    response.IsSuccess = false;
                }

                // Send Email

                using (MailMessage message = new MailMessage())
                {
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(__configuration["ParentEmailAddress:EmailID"]);
                    message.To.Add(new MailAddress(request.EmailId));
                    message.Subject = request.Subject;
                    message.IsBodyHtml = false; //to make message body as html  
                    message.Body = request.body;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(__configuration["ParentEmailAddress:EmailID"], __configuration["ParentEmailAddress:Password"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                __logger.LogError("Error Occur At Repository Layer, Message : " + ex.Message);
                response.IsSuccess = false;
                response.Message = "Error Occur At Repository Layer, Message : " + ex.Message;
            }

            return response;
        }

        public InternetConnectionResponse IsConnectedToInternet()
        {
            InternetConnectionResponse response = new InternetConnectionResponse();
            response.IsSuccess = false;
            string host = "google.com";
            byte[] buffer = Encoding.ASCII.GetBytes("vishal");
            int timeout = 1000;

            using (Ping p = new Ping())
            {
                
                try
                {
                    PingReply reply = p.Send(host, timeout, buffer);
                    if (reply.Status == IPStatus.Success)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "No Internet Connection";
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "Exception At IsConnectedToInternet Method, Error : " + ex.Message;
                    __logger.LogError("Exception At IsConnectedToInternet Method, Error : " + ex.Message);
                    response.IsSuccess = false;
                }
            }
            return response;
        }

        public static string CreateHtmlBody(string EmailID)
        {
            //return "<h1>Hello " + EmailID+"</h1>";
            string htmlString = @"<html>
                      <body>
                      <p>Dear Ms. Susan,</p>
                      <p>Thank you for your letter of yesterday inviting me to come for an interview on Friday afternoon, 5th July, at 2:30.
                              I shall be happy to be there as requested and will bring my diploma and other papers with me.</p>
                      <p>Sincerely,<br>-Jack</br></p>
                      </body>
                      </html>
                     ";
            return htmlString;
        }
    }
}
