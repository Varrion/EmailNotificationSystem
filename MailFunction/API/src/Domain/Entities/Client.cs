namespace API.Domain.Entities;
public class Client : BaseEntity
{
    public int TemplateId { get; set; }

    public required string EmailAddress { get; set; }

    public required string MarketingData { get; set; }

}
