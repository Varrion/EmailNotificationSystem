namespace API.Domain.Entities;
public class EmailConfiguration : BaseEntity
{
    public bool ReceiveMarketingEmails { get; set; }

    public required string EmailAddress { get; set; }
}
