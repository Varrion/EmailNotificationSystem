using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVendingMachine.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrderUseCase;
    private readonly IUpdateOrderUseCase _updateOrderUseCase;
    private readonly IOrderService _orderService;

    public OrderController(
        ICreateOrderUseCase createOrderUseCase,
        IUpdateOrderUseCase updateOrderUseCase,
        IOrderService orderService)
    {
        _createOrderUseCase = createOrderUseCase;
        _updateOrderUseCase = updateOrderUseCase;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        var createdOrder = await _createOrderUseCase.ExecuteAsync(orderDto);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
    {
        var updatedOrder = await _updateOrderUseCase.ExecuteAsync(id, orderDto);
        if (updatedOrder == null)
        {
            return NotFound(); 
        }

        return Ok(updatedOrder);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order?>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        await _orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}
