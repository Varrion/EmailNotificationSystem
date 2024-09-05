namespace API.Domain.Entities;
public class EmailConfiguration : BaseAuditableEntity
{
    public bool ReceiveMarketingEmails { get; set; }

    public required string EmailAddress { get; set; }
}
