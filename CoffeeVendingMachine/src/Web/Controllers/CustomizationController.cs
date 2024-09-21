using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVendingMachine.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomizationController : ControllerBase
{
    private readonly ICustomizationService _customizationService;

    public CustomizationController(ICustomizationService customizationService)
    {
        _customizationService = customizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomizations()
    {
        var customizations = await _customizationService.GetAllCustomizationsAsync();
        return Ok(customizations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomizationById(int id)
    {
        var customization = await _customizationService.GetCustomizationByIdAsync(id);
        if (customization == null)
        {
            return NotFound();
        }
        return Ok(customization);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomization([FromBody] CustomizationDto customizationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _customizationService.AddCustomizationAsync(customizationDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomization(int id, [FromBody] CustomizationDto customizationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _customizationService.UpdateCustomizationAsync(id, customizationDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomization(int id)
    {
        var customization = await _customizationService.GetCustomizationByIdAsync(id);
        if (customization == null)
        {
            return NotFound();
        }

        await _customizationService.DeleteCustomizationAsync(id);
        return NoContent();
    }
}
