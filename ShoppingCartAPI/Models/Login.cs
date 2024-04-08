namespace ShoppingCartAPI.Models
{
    public class Login
    {
        public int login_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int customer_id { get; set; }
        public Customers Customers { get; set; }
    }
}
