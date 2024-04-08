using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class OrdersProductsRepository : IOrdersProductsRepository
    {
        private readonly DataContext _context;
        public ICollection<Cart> cartItems = [];

        public OrdersProductsRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateOrdersProducts(OrdersProducts ordersProducts)
        {
            _context.Add(ordersProducts);
            return Save();
        }

        public bool DeleteOrdersProducts(OrdersProducts ordersProducts)
        {
            _context.Remove(ordersProducts);
            return Save();
        }

        public OrdersProducts GetOrdersProduct(int order_product_id)
        {
            return _context.OrdersProducts.Where(o => o.order_product_id == order_product_id).FirstOrDefault();
        }

        public ICollection<OrdersProducts> GetOrdersProductsByOrderId(int order_id)
        {
            return _context.OrdersProducts.Where(o => o.order_id == order_id).ToList();
        }

        public ICollection<OrdersProducts> GetOrdersProducts()
        {
            return _context.OrdersProducts.OrderBy(op => op.order_product_id).ToList();
        }

        public bool OrdersProductsExists(int order_product_id)
        {
            return _context.OrdersProducts.Any(o => o.order_product_id == order_product_id); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }       
    }
}
