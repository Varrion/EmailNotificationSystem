namespace API.Application.Dto;
public class ClientIdTemplateIdDto
{
    public int ClientId { get; set; }

    public int TemplateId { get; set; }

    public required string TemplateName { get; set; }
}
