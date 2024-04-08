using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Dto
{
    public class CustomersDto
    {
        public int customer_id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        public string full_name { get; set; }
        public string address { get; set; }
    }
}
