using API.Application.Enums;
using API.Application.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MailFunction
{
    public class BulkEmailValidateFunction(ILogger<BulkEmailValidateFunction> logger, IBulkSendEmailUseCase bulkSendEmailUseCase)
    {
        private readonly ILogger<BulkEmailValidateFunction> logger = logger;
        private readonly IBulkSendEmailUseCase bulkSendEmailUse = bulkSendEmailUseCase;

        [Function(nameof(BulkEmailValidateFunction))]
        public async Task Run([BlobTrigger("email-xml-validate/{name}", Connection = "StorageTest")] Stream stream, string name)
        {
            try
            {
                await bulkSendEmailUse.ExecuteAsync(stream, EmailXMLVerificationType.CheckDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(BulkEmailNoValidateFunction));
            }
        }
    }
}
