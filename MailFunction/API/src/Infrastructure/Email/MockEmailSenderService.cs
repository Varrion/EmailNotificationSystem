using API.Application.Interfaces;

namespace API.Infrastructure.Email;
public class MockEmailSenderService() : IEmailSender
{
    private const string LogFileName = "log-email-sent.txt";
    public async Task SendEmailAsync(string from, string recipientEmail, string? subject, string? body)
    {
        var logMessage = $"DateTime: {DateTime.UtcNow}\nFrom: {from}\nRecipient: {recipientEmail}\nSubject: {subject}\nBody:\n{body}\n\n";
        await File.AppendAllTextAsync(LogFileName, logMessage);
    }
}
