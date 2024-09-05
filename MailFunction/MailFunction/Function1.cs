using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MailFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public void Run([QueueTrigger("mailtrigger", Connection = "StorageTest")] QueueMessage message)
        {
            try
            {
                string text = message.Body.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            }
        }
    }
}
