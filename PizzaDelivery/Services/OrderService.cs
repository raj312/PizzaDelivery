using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PizzaDelivery.Constants;
using PizzaDelivery.Interfaces;
using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Services
{
    /*
     * - Typically we should move DB logic to the data layer and only handle business logic here
       - Also implementing an interface for data layer makes it easy to unit test an application
       - for the sake of simplicity, writing DB query logic here
       - Also, use DTOs to transfer data so clients are not aware of the DB models and we can map
        the DTO to the models using tools like auto mapper so we are only updating properties that
        can be changed from the UI
     */
    public class OrderService: IOrderService
    {
        private readonly PizzaDeliveryContext _context;

        public OrderService(
            PizzaDeliveryContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(List<uint> productIds)
        {
            var orderTotal = _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Sum(p => p.Total) ?? 0;

            var order = new Order
            {
                CreatedDate = DateTime.UtcNow,
                Amount = orderTotal * TaxRateConstants.TaxRate
            };

            var addedOrder = _context.Orders.Add(order).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return addedOrder;
        }

        public async Task<Order?> UpdateOrder(int orderId, List<uint> productIds)
        {
            var order = await _context.Orders.FindAsync(orderId).ConfigureAwait(false);

            if (order == null)
            {
                // can throw a order not found custom exception here or let controller / UI handle the NULL
                return null;
            }

            // typically we map from DTO to entity and only update the properties that we need to update.

            var orderTotal = _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Sum(p => p.Total) ?? 0;

            order.Amount = orderTotal * TaxRateConstants.TaxRate;

            var updatedOrder = _context.Orders.Update(order).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return updatedOrder;
        }

        public async Task<Order?> GetOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId).ConfigureAwait(false);
            return order;
        }

        public async Task<Order?> CancelOrder(int orderId)
        {
            // typically we would just update statuses of an order rather than hard delete but for the sake of demo, we will delete the entry
            var order = await GetOrder(orderId).ConfigureAwait(false);
            if (order == null)
            {
                // can throw a order not found custom exception here or let controller / UI handle the NULL
                return null;
            }

            var deletedOrder = _context.Orders.Remove(order).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return deletedOrder;
        }

        public async Task<Order?> CompleteOrder(int orderId)
        {
            var orderToComplete = await GetOrder(orderId).ConfigureAwait(false);
            if (orderToComplete == null)
            {
                // can throw a order not found custom exception here or let controller / UI handle the NULL
                return null;
            }

            orderToComplete.CompletedDate = DateTime.UtcNow;

            var updatedOrder = _context.Orders.Update(orderToComplete).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return updatedOrder;
        }
    }
}
