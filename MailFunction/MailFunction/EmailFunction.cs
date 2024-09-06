using API.Application.Dto;
using API.Application.Interfaces;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MailFunction
{
    public class EmailFunction
    {
        private readonly ILogger<EmailFunction> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SenderDto _senderDto;

        public EmailFunction(ILogger<EmailFunction> logger, IEmailSender emailSender, SenderDto senderDto)
        {
            _logger = logger;
            _emailSender = emailSender;
            _senderDto = senderDto;
        }

        [Function(nameof(EmailFunction))]
        public async Task RunAsync([QueueTrigger("email", Connection = "StorageTest")] QueueMessage message)
        {
            try
            {
                var emailMessage = JsonConvert.DeserializeObject<EmailMessage>(message.Body.ToString());

                if (emailMessage == null)
                {
                    return;
                }

                await _emailSender.SendEmailAsync(_senderDto.Email, emailMessage.To, emailMessage.Title, emailMessage.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(RunAsync));
            }
        }
    }
}
