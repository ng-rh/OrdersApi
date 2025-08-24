using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersDbContext _db;

    public OrdersController(OrdersDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _db.Orders
            .OrderBy(o => o.Id)
            .Select(o => new OrderResponse
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt
            })
            .ToListAsync();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest(new { error = "Order name cannot be empty." });

        var order = new Order { Name = request.Name };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, new OrderResponse
        {
            Id = order.Id,
            Name = order.Name,
            CreatedAt = order.CreatedAt
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return NotFound(new { error = $"Order with id {id} not found." });
        if (string.IsNullOrWhiteSpace(request.Name)) return BadRequest(new { error = "Order name cannot be empty." });

        order.Name = request.Name;
        await _db.SaveChangesAsync();

        return Ok(new OrderResponse
        {
            Id = order.Id,
            Name = order.Name,
            CreatedAt = order.CreatedAt
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return NotFound(new { error = $"Order with id {id} not found." });

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();

        return Ok(new OrderResponse
        {
            Id = order.Id,
            Name = order.Name,
            CreatedAt = order.CreatedAt
        });
    }
}
