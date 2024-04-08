using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Interfaces
{
    public interface IOrdersProductsRepository
    {
        ICollection<OrdersProducts> GetOrdersProducts();
        bool OrdersProductsExists(int order_product_id);
        OrdersProducts GetOrdersProduct(int order_product_id);
        ICollection<OrdersProducts> GetOrdersProductsByOrderId(int order_id);
        bool CreateOrdersProducts(OrdersProducts ordersProducts);
        bool DeleteOrdersProducts(OrdersProducts ordersProducts);
        bool Save();

    }
}
