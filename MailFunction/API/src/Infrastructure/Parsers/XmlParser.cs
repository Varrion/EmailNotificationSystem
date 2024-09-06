using System.Xml;
using System.Xml.Linq;
using API.Application.Dto;
using API.Application.Interfaces;
using API.Domain.Entities;
using Newtonsoft.Json;

namespace API.Infrastructure.Parsers;
public class XmlParser : IXmlParser
{
    public async Task<List<Client>> ParseClientsFromXmlAsync(string filePath)
    {
        var xmlContent = await File.ReadAllTextAsync(filePath);
        var xDocument = XDocument.Parse(xmlContent); // Parse the XML content
        var clients = new List<Client>();

        foreach (var clientElement in xDocument.Descendants("Client"))
        {
            var clientId = clientElement.Attribute("ID")?.Value;

            if (string.IsNullOrWhiteSpace(clientId) || !int.TryParse(clientId, out int parsedClientId))
            {
                continue;
            }

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

                var client = new Client
                {
                    Id = parsedClientId,
                    TemplateId = parsedTemplateId,
                    MarketingData = marketingData,
                    EmailAddress = "example@example.com"
                };

                clients.Add(client);
            }
        }

        return clients;
    }

    public async Task<List<ClientMarketingDataDto>> ParseClientTemplateFromXmlAsync(string filePath)
    {
        var clientDataList = new List<ClientMarketingDataDto>();

        using (var reader = XmlReader.Create(filePath))
        {
            while (await reader.ReadAsync())
            {
                // Look for the Client node
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Client")
                {
                    var clientId = reader.GetAttribute("ID");

                    if (string.IsNullOrWhiteSpace(clientId) || !int.TryParse(clientId, out int parsedClientId))
                    {
                        continue;
                    }

                    if (reader.ReadToFollowing("Template"))
                    {
                        var templateName = string.Empty;
                        var marketingData = string.Empty;

                        if (reader.ReadToFollowing("Name"))
                        {
                            templateName = await reader.ReadElementContentAsStringAsync();
                        }

                        if (reader.ReadToFollowing("MarketingData"))
                        {
                            marketingData = await reader.ReadElementContentAsStringAsync();
                        }

                        try
                        {
                            clientDataList.Add(new ClientMarketingDataDto
                            {
                                ClientId = parsedClientId,
                                MarketingData = JsonConvert.DeserializeObject<MarketingData>(marketingData)!
                            });
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
        }

        return clientDataList;
    }

    public async Task<List<ClientIdTemplateIdDto>> ParseClientTemplateIdFromXmlAsync(string filePath)
    {
        var clientDataList = new List<ClientIdTemplateIdDto>();

        using (var reader = XmlReader.Create(filePath))
        {
            while (await reader.ReadAsync())
            {
                // Look for the Client node
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Client")
                {
                    // Get the Client ID and validate it
                    var clientId = reader.GetAttribute("ID");
                    if (string.IsNullOrWhiteSpace(clientId) || !int.TryParse(clientId, out int parsedClientId))
                    {
                        continue;
                    }

                    if (reader.ReadToFollowing("Template"))
                    {
                        var templateId = reader.GetAttribute("Id");
                        if (string.IsNullOrWhiteSpace(templateId) || !int.TryParse(templateId, out int parsedTemplateId))
                        {
                            continue;
                        }

                        var templateName = string.Empty;

                        if (reader.ReadToFollowing("Name"))
                        {
                            templateName = await reader.ReadElementContentAsStringAsync();
                        }

                        // Add parsed client and template ID to the list
                        clientDataList.Add(new ClientIdTemplateIdDto
                        {
                            ClientId = parsedClientId,
                            TemplateId = parsedTemplateId,
                            TemplateName = templateName
                        });
                    }
                }
            }
        }

        return clientDataList;
    }
}
