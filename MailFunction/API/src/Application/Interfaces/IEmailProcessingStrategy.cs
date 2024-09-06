namespace API.Application.Interfaces;
public interface IEmailProcessingStrategy
{
    Task ExecuteAsync(Stream xmlStream);
}
