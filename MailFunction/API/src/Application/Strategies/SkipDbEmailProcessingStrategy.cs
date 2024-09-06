using API.Application.Dto;
using API.Application.Interfaces;

namespace API.Application.Strategies;
public class SkipDbEmailProcessingStrategy : IEmailProcessingStrategy
{
    private readonly IXmlParser _xmlParser;
    private readonly IEmailSender _emailSender;
    private readonly IClientRepository _clientRepository;
    private readonly SenderDto _senderDto;

    public SkipDbEmailProcessingStrategy(IXmlParser xmlParser, IEmailSender emailSender, IClientRepository clientRepository, SenderDto senderDto)
    {
        _xmlParser = xmlParser;
        _emailSender = emailSender;
        _clientRepository = clientRepository;
        _senderDto = senderDto;
    }

    public async Task ExecuteAsync(string xmlFilePath)
    {
        var clientMarketingDataList = await _xmlParser.ParseClientTemplateFromXmlAsync(xmlFilePath);

        var tasks = new List<Task>();
        foreach (var clientMarketingData in clientMarketingDataList)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientMarketingData.ClientId);

            if (client == null)
            {
                continue;
            }

            if (clientMarketingData.MarketingData == null)
            {
                continue;
            }

            tasks.Add(_emailSender.SendEmailAsync(
                 _senderDto.Email,
                client.EmailAddress,
                clientMarketingData.MarketingData.Content,
                clientMarketingData.MarketingData.Title
            ));
        }

        await Task.WhenAll(tasks);
    }
}
