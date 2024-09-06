using API.Application.Enums;
using API.Application.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MailFunction
{
    public class BulkEmailNoValidateFunction(ILogger<BulkEmailNoValidateFunction> logger, IBulkSendEmailUseCase bulkSendEmailUseCase)
    {
        private readonly ILogger<BulkEmailNoValidateFunction> logger = logger;
        private readonly IBulkSendEmailUseCase bulkSendEmailUse = bulkSendEmailUseCase;

        [Function(nameof(BulkEmailNoValidateFunction))]
        public async Task Run([BlobTrigger("email-xml-novalidate/{name}", Connection = "StorageTest")] Stream stream, string name)
        {
            try
            {
                //using var blobStreamReader = new StreamReader(stream, Encoding.UTF8);
                //var content = await blobStreamReader.ReadToEndAsync().ConfigureAwait(false);

                //if (string.IsNullOrWhiteSpace(content))
                //{
                //    return;
                //}

                await bulkSendEmailUse.ExecuteAsync(stream, EmailXMLVerificationType.SkipDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(BulkEmailNoValidateFunction));
            }
        }
    }
}