using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class Products
    {
        public int product_id {  get; set; }
        
        [Required]
        public string product_name { get; set; }
        public int product_price { get; set; }
        
        [Required]
        public string product_img { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<OrdersProducts> OrdersProducts { get; set; }
    }
}
