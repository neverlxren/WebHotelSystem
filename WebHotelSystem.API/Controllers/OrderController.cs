using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("order/orders")]
    public async Task<IActionResult> GetOrderInfoAsync(int id)
    {
        var order = await _webHotelService.GetOrderInfoById(id);

        if (order is null) return NotFound(new { message = "sorry, can't find this one" });
        return Ok(order);
    }
    
    /*[HttpPatch("{id}")]
    public async Task<IActionResult> ChangeOrderStatusAsync(ChangeOrder dto, int id)
    {
        var change = _webHotelService.ChangeOrderStatus(id);

        if (сhange is null) return NotFound();
        
    }*/
}