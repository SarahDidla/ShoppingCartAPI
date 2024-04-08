using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Interfaces
{
    public interface IOrdersRepository
    {
        ICollection<Orders> GetOrders();
        Orders GetOrderById(int id);
        bool OrderExists(int id);
        bool Save();
        bool PlaceOrder(CartOrderRequest cartOrderRequest);
        bool CreateOrder(Orders order);
        ICollection<OrdersDetails> GetOrderDetailsByCustomerId(int customerId);
        bool DeleteOrder(Orders order);
    }
}
