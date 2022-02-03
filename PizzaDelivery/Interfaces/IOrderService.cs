using PizzaDelivery.Models.Entities;

namespace PizzaDelivery.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(List<uint> productIds);
        Task<Order?> UpdateOrder(uint orderId, List<uint> productIds);
        Task<Order?> GetOrder(uint orderId);
        Task<Order?> CancelOrder(uint order);
        Task<Order?> CompleteOrder(uint orderId);
    }
}
