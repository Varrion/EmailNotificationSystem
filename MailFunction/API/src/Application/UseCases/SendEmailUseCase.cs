using API.Application.Interfaces;
using API.Domain.Entities;

namespace API.Application.UseCases;
public class SendEmailUseCase(IEmailSender emailSender, ITemplateRepository templateRepository, IClientRepository clientRepository)
{
    public async Task ExecuteAsync(int clientId, int templateId, string marketingData)
    {
        // Fetch client configuration
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if (client == null || !client.Configuration.ReceiveMarketingEmails)
            return;

        // Fetch email template
        var template = await templateRepository.GetTemplateByIdAsync(templateId);
        if (template == null)
            throw new Exception("Template not found");

        // Prepare email body by injecting marketing data into the template
        var emailBody = RenderTemplate(template, marketingData);

        // Send the email
        await emailSender.SendEmailAsync(client.Configuration.EmailAddress, "Your Marketing Email", emailBody);
    }

    private string RenderTemplate(Template template, string marketingData)
    {
        // Inject marketingData into the template. Use something like RazorLight, or replace placeholders
        return template.Content.Replace("{{MarketingData}}", marketingData);
    }

}
