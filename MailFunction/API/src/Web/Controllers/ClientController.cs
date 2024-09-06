using API.Application.Dto;
using API.Application.Interfaces;
using API.Domain.Entities;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly QueueClient _queueClient;
    private readonly BlobServiceClient _blobServiceClient;


    public ClientController(IClientService clientService, IConfiguration configuration)
    {
        _clientService = clientService;

        var queueConnectionString = configuration.GetConnectionString("AzureStorageConnectionString");
        _queueClient = new QueueClient(queueConnectionString, "email");
        _blobServiceClient = new BlobServiceClient(queueConnectionString);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] Client client)
    {
        if (client == null)
        {
            return BadRequest("Invalid client data.");
        }

        await _clientService.CreateClientAsync(client);
        return Ok("Client created successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound($"Client with Id {id} not found.");
        }
        return Ok(client);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClient([FromBody] Client client)
    {
        if (client == null)
        {
            return BadRequest("Invalid client data.");
        }

        await _clientService.UpdateClientAsync(client);
        return Ok("Client updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound($"Client with Id {id} not found.");
        }

        await _clientService.DeleteClientAsync(id);
        return Ok("Client deleted successfully.");
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> TriggerClientAction([FromBody] EmailMessageRequest emailMessageRequest)
    {
        await _queueClient.CreateIfNotExistsAsync();
        var emailContent = JsonConvert.DeserializeObject<MarketingData>(emailMessageRequest.MarketingData);

        if (emailContent == null)
        {
            return Ok();
        }

        var emailMessage = new EmailMessage
        {
            To = emailMessageRequest.To,
            Title = emailContent.Title,
            Content = emailContent.Content
        };

        var jsonObject = JsonConvert.SerializeObject(emailMessage);
        await _queueClient.SendMessageAsync(jsonObject);

        return Ok($"Email sent for {emailMessage.To}, message added to queue.");
    }

    [HttpPost("upload-xml")]
    public async Task<IActionResult> UploadXmlFile(IFormFile file, [FromForm] bool validate)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid XML file.");
        }

        string containerName = validate ? "email-xml-validate" : "email-xml-novalidate";

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync();
        var blobClient = blobContainerClient.GetBlobClient(file.FileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return Ok("File uploaded successfully.");
    }
}
