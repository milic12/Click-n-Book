using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Click_and_Book.Email
{
    public class SendGidEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SendGidEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            var apiKey = _configuration["SendGridKey"];

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(details.FromEmail, details.FromName);

            var to = new EmailAddress(details.ToEmail, details.ToName);

            var subject = details.Subject;

            var content = details.Content;
            
            var msg = MailHelper.CreateSingleEmail(
                from, 
                to, 
                subject, 
                details.IsHTML ? null : content, 
                details.IsHTML ? content : null);

            if (details.ReplayToEmail != null)
                msg.SetReplyTo(new EmailAddress(details.ReplayToEmail, details.ReplayToName));

            msg.TemplateId = details.TemplateId;
            
            msg.SetTemplateData(details.TemplateData);

            var response = await client.SendEmailAsync(msg);

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return new SendEmailResponse();
            }

            try
            {
                var bodyResult = await response.Body.ReadAsStringAsync();

                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

                var errorReponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                if (errorReponse.Errors == null || errorReponse.Errors.Count == 0)
                {
                    errorReponse.Errors = new List<string>(new[]
                    {
                        "Unknown error from email sending service."
                    });
                }

                return errorReponse;
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    var error = ex;
                    Debugger.Break();

                }

                return new SendEmailResponse
                {
                    Errors = new List<string>(new[]
                    {
                        "Unknown error occurred"
                    })
                };
            }
        }
    }
}
