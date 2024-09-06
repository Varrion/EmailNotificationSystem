namespace API.Application.Interfaces;
public interface IEmailProcessingStrategy
{
    Task ExecuteAsync(string xmlFilePath);
}
