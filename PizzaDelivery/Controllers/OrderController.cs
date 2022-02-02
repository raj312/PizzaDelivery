using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaDelivery.Interfaces;
using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Controllers
{
    // All controllers should have authentication and authorization logic
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region private fields

        private readonly IOrderService _orderService;

        #endregion
        public OrderController(
            IOrderService orderService
        )
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrder(orderId).ConfigureAwait(false);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            var createdOrder = await _orderService.CreateOrder(order).ConfigureAwait(false);
            return Ok(createdOrder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var createdOrder = await _orderService.UpdateOrder(order).ConfigureAwait(false);
            return Ok(createdOrder);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var createdOrder = await _orderService.CancelOrder(orderId).ConfigureAwait(false);
            return Ok(createdOrder);
        }

    }
}
