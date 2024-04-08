using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetCart();
        ICollection<Cart> GetCartByCustomerId(int customer_id);
        Cart GetCart(int cart_id);
        bool CartProductExists(int id);
        bool Save();
        bool AddToCart(Cart cart);
        bool DeleteItem(Cart cartItem);
        ICollection<Cart> CheckOut(ICollection<Cart> cartItems);
    }
}
