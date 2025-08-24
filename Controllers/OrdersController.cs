using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace OrdersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, Order> Orders = new();
        private static int _nextId = 0;

        // GET: api/orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            var response = Orders.Values
                .OrderBy(o => o.Id)
                .Select(o => new OrderResponse
                {
                    Id = o.Id,
                    Name = o.Name,
                    CreatedAt = o.CreatedAt
                });

            return Ok(response);
        }

        // POST: api/orders
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { error = "Order name cannot be empty." });
            }

            var order = new Order
            {
                Id = Interlocked.Increment(ref _nextId),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            Orders[order.Id] = order;

            var response = new OrderResponse
            {
                Id = order.Id,
                Name = order.Name,
                CreatedAt = order.CreatedAt
            };

            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, response);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { error = "Order name cannot be empty." });
            }

            if (Orders.TryGetValue(id, out var existing))
            {
                existing.Name = request.Name;

                var response = new OrderResponse
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    CreatedAt = existing.CreatedAt
                };

                return Ok(response);
            }

            return NotFound(new { error = $"Order with id {id} not found." });
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            if (Orders.TryRemove(id, out var removed))
            {
                var response = new OrderResponse
                {
                    Id = removed.Id,
                    Name = removed.Name,
                    CreatedAt = removed.CreatedAt
                };

                return Ok(response);
            }

            return NotFound(new { error = $"Order with id {id} not found." });
        }
    }

    // === Entities & DTOs ===

    // Internal entity
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

    // Requests
    public class CreateOrderRequest
    {
        public string Name { get; set; } = "";
    }

    public class UpdateOrderRequest
    {
        public string Name { get; set; } = "";
    }

    // Responses
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
