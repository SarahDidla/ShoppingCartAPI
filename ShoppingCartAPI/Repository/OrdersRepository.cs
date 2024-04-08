using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _context;

        public OrdersRepository(DataContext context)
        {
            _context = context;
        }
        public Orders GetOrderById(int id)
        {
            return _context.Orders.Where(o => o.order_id == id).FirstOrDefault();
        }

        public ICollection<Orders> GetOrders()
        {
            return _context.Orders.OrderBy(o => o.order_id).ToList();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.order_id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool PlaceOrder(CartOrderRequest cartOrderRequest)
        {
            var newOrder = new Orders
            {
                customer_id = cartOrderRequest.customer_id,
                total_amount = cartOrderRequest.total_amount,
                shipping_address = cartOrderRequest.shipping_address,
                payment_method = cartOrderRequest.payment_method
            };


            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            foreach (var cartItem in cartOrderRequest.cartItems)
            {
                var newOrdersProducts = new OrdersProducts
                {
                    product_id = cartItem.product_id,
                    order_id = newOrder.order_id,
                    product_name = cartItem.product_name
                };

                _context.OrdersProducts.Add(newOrdersProducts);
            }

            return Save();
        }

        public ICollection<OrdersDetails> GetOrderDetailsByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.customer_id == customerId)
                .Select(o => new OrdersDetails
                {
                    order_id = o.order_id,
                    customer_id = o.customer_id,
                    OrdersProductsDetails = o.OrdersProducts.Select(op => new OrdersProductsDetails
                    {
                        product_id = op.product_id,
                        product_name = op.Products.product_name,
                        product_price = op.Products.product_price,
                        product_img = op.Products.product_img
                    }).ToList()
                }).ToList();
        }
        public bool DeleteOrder(Orders order)
        {
            _context.Remove(order);
            return Save();
        }

        public bool CreateOrder(Orders order)
        {
            _context.Add(order);
            return Save();
        }
    }
}
