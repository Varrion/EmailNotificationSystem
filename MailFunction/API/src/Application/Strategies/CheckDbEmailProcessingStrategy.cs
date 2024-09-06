using API.Application.Dto;
using API.Application.Interfaces;
using Newtonsoft.Json;

namespace API.Application.Strategies;
public class CheckDbEmailProcessingStrategy : IEmailProcessingStrategy
{
    private readonly IXmlParser _xmlParser;
    private readonly ITemplateRepository _templateRepository;
    private readonly IClientRepository _clientRepository;
    private readonly SenderDto _senderDto;
    private readonly IEmailSender _emailSender;

    public CheckDbEmailProcessingStrategy(IXmlParser xmlParser, ITemplateRepository templateRepository, SenderDto senderDto, IClientRepository clientRepository, IEmailSender emailSender)
    {
        _xmlParser = xmlParser;
        _templateRepository = templateRepository;
        _senderDto = senderDto;
        _clientRepository = clientRepository;
        _emailSender = emailSender;
    }

    public async Task ExecuteAsync(Stream xmlStream)
    {
        var clientsWithTemplateId = await _xmlParser.ParseClientTemplateIdFromXmlAsync(xmlStream); // Parse ClientId and TemplateId

        var tasks = new List<Task>();
        foreach (var clientTemplate in clientsWithTemplateId)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientTemplate.ClientId);

            if (client == null)
            {
                continue;
            }

            var template = await _templateRepository.GetTemplateByIdAsync(clientTemplate.TemplateId);
            if (template == null)
            {
                continue;
            }

            var marketingData = JsonConvert.DeserializeObject<MarketingData>(template.MarketingData);

            if (marketingData == null)
            {
                continue;
            }

            tasks.Add(_emailSender.SendEmailAsync(
                _senderDto.Email,
                client.EmailAddress,
                marketingData.Content,
                marketingData.Title
            ));
        }

        await Task.WhenAll(tasks);
    }
}
