using API.Application.Interfaces;

namespace API.Infrastructure.Email;
public class EmailSenderService : IEmailSender
{
    public Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
