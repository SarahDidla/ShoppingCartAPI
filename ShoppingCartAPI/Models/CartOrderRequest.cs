using ShoppingCartAPI.Dto;

namespace ShoppingCartAPI.Models
{
    // The information contained in the order sent by the customer
    public class CartOrderRequest
    {
        public int customer_id { get; set; }
        public int total_amount { get; set; }
        public string shipping_address { get; set; }
        public string payment_method { get; set; }
        public List<CartDto> cartItems { get; set; }

    }
}
