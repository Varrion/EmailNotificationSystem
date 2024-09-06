using API.Application.Interfaces;

namespace API.Infrastructure.Email;
public class MockEmailSenderService(string logFilePath) : IEmailSender
{
    public async Task SendEmailAsync(string from, string recipientEmail, string? subject, string? body)
    {
        var logMessage = $"From: {from}\nRecipient: {recipientEmail}\nSubject: {subject}\nBody:\n{body}\n\n";
        await File.AppendAllTextAsync(logFilePath, logMessage);
    }
}
