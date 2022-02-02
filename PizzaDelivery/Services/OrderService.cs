using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PizzaDelivery.Interfaces;
using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Services
{
    public class OrderService: IOrderService
    {
        // typically we should move DB logic to the data layer and only handle business logic here
        // also implementing an interface for data layer makes it easy to unit test an application
        private readonly PizzaDeliveryContext _context;

        public OrderService(
            PizzaDeliveryContext context)
        {
            _context = context;
        }
        public Task<Order> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId).ConfigureAwait(false);
            return order;
        }

        public Task<Order> CancelOrder(int order)
        {
            throw new NotImplementedException();
        }
    }
}
