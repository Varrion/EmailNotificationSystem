﻿using API.Application.Interfaces;

namespace API.Infrastructure.Email;
public class MockEmailSenderService(string logFilePath) : IEmailSender
{
    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        // Log the email details to a file for testing purposes
        var logMessage = $"Recipient: {recipientEmail}\nSubject: {subject}\nBody:\n{body}\n\n";

        await File.AppendAllTextAsync(logFilePath, logMessage);
    }
}
