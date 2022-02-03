using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(List<uint> productIds);
        Task<Order?> UpdateOrder(int orderId, List<uint> productIds);
        Task<Order?> GetOrder(int orderId);
        Task<Order?> CancelOrder(int order);
        Task<Order?> CompleteOrder(int orderId);
    }
}
