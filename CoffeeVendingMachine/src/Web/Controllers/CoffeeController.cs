using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVendingMachine.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoffeeController : ControllerBase
{
    private readonly IFetchExternalCoffeesUseCase _fetchExternalCoffeesUseCase;
    private readonly IFetchSingleExternalCoffeeUseCase _fetchSingleExternalCoffeeUseCase;
    private readonly ICoffeeService _coffeeService;

    public CoffeeController(IFetchExternalCoffeesUseCase fetchExternalCoffeesUseCase, IFetchSingleExternalCoffeeUseCase fetchSingleExternalCoffeeUseCase, ICoffeeService coffeeService)
    {
        _fetchExternalCoffeesUseCase = fetchExternalCoffeesUseCase;
        _fetchSingleExternalCoffeeUseCase = fetchSingleExternalCoffeeUseCase;
        _coffeeService = coffeeService;
    }

    [HttpGet("external-coffees")]
    public async Task<IActionResult> GetExternalCoffees()
    {
        var coffees = await _fetchExternalCoffeesUseCase.ExecuteAsync();
        return Ok(coffees);
    }

    [HttpGet("external-coffees/{id}")]
    public async Task<IActionResult> GetExternalCoffeeById(int id)
    {
        var coffee = await _fetchSingleExternalCoffeeUseCase.ExecuteAsync(id);

        return coffee == null 
            ? NotFound() 
            : Ok(coffee);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocalCoffees()
    {
        var coffees = await _coffeeService.GetLocalCoffeesAsync();
        return Ok(coffees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocalCoffeeById(int id)
    {
        var coffee = await _coffeeService.GetLocalCoffeeByIdAsync(id);
        if (coffee == null)
        {
            return NotFound();
        }
        return Ok(coffee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocalCoffee([FromBody] CoffeeDto coffeeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _coffeeService.AddCoffeeAsync(coffeeDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocalCoffee(int id, [FromBody] CoffeeDto coffeeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _coffeeService.UpdateCoffeeAsync(id, coffeeDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocalCoffee(int id)
    {
        var coffee = await _coffeeService.GetLocalCoffeeByIdAsync(id);
        if (coffee == null)
        {
            return NotFound();
        }

        await _coffeeService.DeleteCoffeeAsync(id);
        return NoContent();
    }
}
