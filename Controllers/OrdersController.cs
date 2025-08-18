using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<string> Orders = new();

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(Orders);
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] string order)
    {
        Orders.Add(order);
        return Created("", order);
    }
}
