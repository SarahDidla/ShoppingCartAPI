using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        public ICollection<Cart> cartItems = [];

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CartProductExists(int id)
        {
            return _context.Cart.Any(x => x.cart_id == id);
        }

        public ICollection<Cart> GetCart()
        {
            return _context.Cart.OrderBy(x => x.cart_id).ToList();
        }

        public Cart GetCart(int cart_id)
        {
            return _context.Cart.Where(c => c.cart_id == cart_id).FirstOrDefault();
        }

        // Returns the cart items related to a specific customer
        public ICollection<Cart> GetCartByCustomerId(int customer_id)
        {
            return _context.Cart.Where(c => c.customer_id == customer_id).ToList();
        }


        /*public bool AddToCart(int productId, int customer_id)
        {
            var product = _context.Products.FirstOrDefault(p => p.product_id == productId);

            if (product == null)
            {
                return false;
            }

            var newCartItem = new Cart
            {
                product_id = productId,
                product_name = product.product_name,
                product_price = product.product_price,
                customer_id = customer_id
            };

            _context.Cart.Add(newCartItem);

            return Save();
        }*/

        public bool AddToCart(Cart cart)
        {
            _context.Cart.Add(cart);
            return Save();
        }
        public bool DeleteItem(Cart cartItem)
        {
            _context.Remove(cartItem);
            return Save();
        }

        public ICollection<Cart> CheckOut(ICollection<Cart> cartItems)
        {
            this.cartItems = cartItems;
            return cartItems;
        }

        /*public bool PlaceOrder(int customer_id, int total_amount, string shipping_address, string payment_method)
        {
            var newOrder = new Orders
            {
                customer_id = customer_id,
                total_amount = total_amount,
                shipping_address = shipping_address,
                payment_method = payment_method
            };

            _context.Orders.Add(newOrder);
            return Save();
        }*/
    }
}
