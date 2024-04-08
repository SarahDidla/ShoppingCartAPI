namespace ShoppingCartAPI.Models
{
    public class OrdersProductsDetails
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public string product_img { get; set; }
    }
}
