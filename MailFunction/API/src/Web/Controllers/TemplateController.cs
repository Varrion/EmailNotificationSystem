using API.Application.Interfaces;
using API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplateController : ControllerBase
{
    private readonly ITemplateService _templateService;

    public TemplateController(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] Template template)
    {
        await _templateService.CreateTemplateAsync(template);
        return Ok("Template created successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTemplate(int id)
    {
        var template = await _templateService.GetTemplateByIdAsync(id);
        if (template == null)
        {
            return NotFound();
        }
        return Ok(template);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTemplates()
    {
        var templates = await _templateService.GetAllTemplatesAsync();
        return Ok(templates);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTemplate([FromBody] Template template)
    {
        await _templateService.UpdateTemplateAsync(template);
        return Ok("Template updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(int id)
    {
        await _templateService.DeleteTemplateAsync(id);
        return Ok("Template deleted successfully.");
    }
}
