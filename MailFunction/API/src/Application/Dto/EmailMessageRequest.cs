namespace API.Application.Dto;
public class EmailMessageRequest
{
    public required string To { get; set; }
    public required string MarketingData { get; set; }
}
