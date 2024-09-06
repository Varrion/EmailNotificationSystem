using System.Xml;
using API.Application.Dto;
using API.Application.Interfaces;
using Newtonsoft.Json;

namespace API.Infrastructure.Parsers;
public class XmlParser : IXmlParser
{
    public async Task<List<ClientMarketingDataDto>> ParseClientTemplateFromXmlAsync(Stream xmlStream)
    {
        try
        {
            var clientDataList = new List<ClientMarketingDataDto>();
            var settings = new XmlReaderSettings
            {
                Async = true
            };
            using (var reader = XmlReader.Create(xmlStream, settings))
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
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<ClientIdTemplateIdDto>> ParseClientTemplateIdFromXmlAsync(Stream xmlStream)
    {
        var clientDataList = new List<ClientIdTemplateIdDto>();
        var settings = new XmlReaderSettings
        {
            Async = true
        };
        using (var reader = XmlReader.Create(xmlStream, settings))
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
