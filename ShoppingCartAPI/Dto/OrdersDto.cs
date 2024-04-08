namespace ShoppingCartAPI.Dto
{
    public class OrdersDto
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public decimal total_amount { get; set; }
        public string shipping_address { get; set; }
        public string payment_method { get; set; }
    }
}
