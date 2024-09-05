using API.Application.Interfaces;
using API.Domain.Entities;
using System.Xml.Linq;

namespace API.Infrastructure.Parsers;
public class XmlParser : IXmlParser
{
    public async Task<List<Client>> ParseClientsFromXmlAsync(string filePath)
    {
        var xmlContent = await File.ReadAllTextAsync(filePath);
        var xDocument = XDocument.Parse(xmlContent); // Parse the XML content
        var clients = new List<Client>();

        // Iterate through each Client element in the XML
        foreach (var clientElement in xDocument.Descendants("Client"))
        {
            // Parse Client ID
            var clientId = clientElement.Attribute("ID")?.Value;

            if (string.IsNullOrWhiteSpace(clientId) || !int.TryParse(clientId, out int parsedClientId))
            {
                continue;
            }

            // Parse Template element for TemplateId and MarketingData
            var templateElement = clientElement.Element("Template");
            if (templateElement != null)
            {
                var templateId = templateElement.Attribute("Id")?.Value;

                if (string.IsNullOrWhiteSpace(templateId) || !int.TryParse(clientId, out int parsedTemplateId))
                {
                    continue;
                }

                var marketingData = templateElement.Element("MarketingData")?.Value;

                if (string.IsNullOrWhiteSpace(marketingData))
                {
                    continue;
                }

                // Build Client object and add to the list
                var client = new Client
                {
                    Id = parsedClientId,
                    TemplateId = parsedTemplateId, // Convert TemplateId to int
                    MarketingData = marketingData, // Assign the MarketingData from XML
                    Configuration = new EmailConfiguration
                    {
                        // Here, you can populate the EmailConfiguration fields as needed,
                        // You could also add more XML fields to populate the EmailAddress if available
                        ReceiveMarketingEmails = true, // Example setting, adjust as needed
                        EmailAddress = "example@example.com" // Set a default email or pull it from XML
                    }
                };

                clients.Add(client);
            }
        }

        return clients;
    }
}
