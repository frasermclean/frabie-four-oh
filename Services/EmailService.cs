using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace FrabieFourOh.Services
{
    public interface IEmailService
    {
        Task<bool> SendInvitationAsync(string name, string email, string code);
    }

    public class EmailService : IEmailService
    {
        private readonly string templateId;
        private readonly SendGridClient client;

        public EmailService(IConfiguration configuration)
        {
            // read api key from configuration
            IConfigurationSection section = configuration.GetSection("SendGrid");
            string apiKey = section.GetValue<string>("ApiKey");
            templateId = section.GetValue<string>("TemplateId");

            // create send grid client
            client = new SendGridClient(apiKey);
        }

        public async Task<bool> SendInvitationAsync(string name, string email, string code)
        {
            EmailAddress fromAddress = new("louiseyoung@frasermclean.com", "Louise and Fraser");
            EmailAddress toAddress = new(email, name);

            // set dynamic template data
            var data = new
            {
                name,
                email,
                code,
            };

            SendGridMessage message = MailHelper.CreateSingleTemplateEmail(fromAddress, toAddress, templateId, data);
            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}
