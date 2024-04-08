namespace ShoppingCartAPI.Models
{
    public class Cart
    {
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int product_price { get; set; }
        public int customer_id { get; set; }
        public Products Product { get; set; }
        public Customers Customer { get; set; }

    }
}
