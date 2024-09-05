using API.Application.Interfaces;

namespace API.Application.UseCases;
public class BulkSendEmailUseCase(IXmlParser xmlParser, SendEmailUseCase sendEmailUseCase)
{
    public async Task ExecuteAsync(string xmlFilePath)
    {
        // Parse clients from XML
        var clients = await xmlParser.ParseClientsFromXmlAsync(xmlFilePath);

        // Send emails in parallel
        var tasks = new List<Task>();
        foreach (var client in clients)
        {
            var task = sendEmailUseCase.ExecuteAsync(client.Id, client.TemplateId, client.MarketingData);
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
}
