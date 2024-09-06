namespace API.Application.Dto;
public class ClientMarketingDataDto
{
    public int ClientId { get; set; }
    public required MarketingData MarketingData { get; set; }
}
