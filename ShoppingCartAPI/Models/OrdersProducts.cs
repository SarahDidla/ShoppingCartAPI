namespace ShoppingCartAPI.Models
{
    public class OrdersProducts
    {
        public int order_product_id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
    }
}
