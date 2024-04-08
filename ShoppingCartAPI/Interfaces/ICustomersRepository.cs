using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Interfaces
{
    public interface ICustomersRepository
    {
        ICollection<Customers> GetCustomers();
        Customers GetCustomer(int id);
        bool CustomerExists(int customer_id);
        Customers GetCustomerByUsername(string username);
        bool VerifyPassword(string username, string password);
        bool Register(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(Customers customer);
    }
}
