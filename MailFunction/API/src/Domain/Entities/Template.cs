namespace API.Domain.Entities;
public class Template : BaseAuditableEntity
{
    public required string Name { get; set; }

    public required string MarketingData { get; set; }
}
