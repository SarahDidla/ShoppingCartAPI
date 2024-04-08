using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class Customers
    {
        public int customer_id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string full_name { get; set; }
        public string address { get; set; }

        public ICollection<Cart> Cart { get; set; }
        public Login Login { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}