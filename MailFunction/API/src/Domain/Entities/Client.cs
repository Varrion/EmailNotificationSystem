namespace API.Domain.Entities;
public class Client : BaseAuditableEntity
{
    public required EmailConfiguration Configuration { get; set; }
    public int TemplateId { get; set; }
    public required string MarketingData { get; set; }

}
