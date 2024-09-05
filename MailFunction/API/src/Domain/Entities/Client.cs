namespace API.Domain.Entities;
public class Client : BaseAuditableEntity
{
    public required EmailConfiguration Configuration { get; set; }
    public int TemplateId { get; set; }  // This should be part of Client for a specific email
    public required string MarketingData { get; set; }  // This contains dynamic data for the email

}
