using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> GetOrder(int orderId);
        Task<Order> CancelOrder(int order);
    }
}
