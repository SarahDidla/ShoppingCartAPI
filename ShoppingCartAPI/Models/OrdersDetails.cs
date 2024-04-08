namespace ShoppingCartAPI.Models
{
    public class OrdersDetails
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public ICollection<OrdersProductsDetails> OrdersProductsDetails { get; set; }
    }
}
