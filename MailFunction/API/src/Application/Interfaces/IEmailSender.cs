namespace API.Application.Interfaces;
public interface IEmailSender
{
    Task SendEmailAsync(string from, string recipientEmail, string? subject, string? body);
}
