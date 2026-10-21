using Microsoft.AspNetCore.Mvc;
using WebHotelSystem.API.DTOs;
using WebHotelSystem.API.Services;

namespace WebHotelSystem.API.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IWebHotelService _webHotelService;

    public OrderController(IWebHotelService webHotelService)
    {
        _webHotelService = webHotelService;
    }

    [HttpGet("view/order={id:int}")]
    public async Task<IActionResult> GetOrderInfoAsync(int id)
    {
        var order = await _webHotelService.GetOrderInfoById(id);

        if (order is null) return NotFound(new { message = "sorry, can't find this one" });
        return Ok(order);
    }
    
    [HttpPatch("edit/order={id:int}&status={status}")]
    public async Task<IActionResult> ChangeOrderStatus(int id, string status)
    {
        var order = await _webHotelService.ChangeOrderStatus(id, status);

        if (order is null)
            return NotFound();
        return Ok(order);
    }

    [HttpPost("create/order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var order = await _webHotelService.CreateOrder(dto);
        if (order is null) return Conflict(new { message = "smtng goes wrong" });
        return Ok(order);
    }

    [HttpGet("view/orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orderlist = await _webHotelService.GetAllOrders();

        return Ok(orderlist);
    }
}