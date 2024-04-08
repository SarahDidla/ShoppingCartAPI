namespace ShoppingCartAPI.Dto
{
    public class OrdersProductsDto
    {
        public int order_product_id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
    }
}
