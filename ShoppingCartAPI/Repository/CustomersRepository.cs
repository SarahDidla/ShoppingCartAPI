using Microsoft.AspNetCore.Components.Web;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DataContext _context;

        public CustomersRepository(DataContext context)
        {
            _context = context;
        }

        public bool CustomerExists(int customer_id)
        {
            return _context.Customers.Any(c => c.customer_id == customer_id);
        }

        public Customers GetCustomer(int id)
        {
            return _context.Customers.Where(c => c.customer_id == id).FirstOrDefault();
        }

        public ICollection<Customers> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.customer_id).ToList();
        }

        public Customers GetCustomerByUsername(string username)
        {
            return _context.Customers.Where(c => c.username == username).FirstOrDefault();
        }

        public bool VerifyPassword(string username, string password)
        {
            var customer = GetCustomerByUsername(username);

            if (customer == null || customer.password != password) 
            {
                return false;
            }

            return true;
        }

        // Creates a new customer record
        public bool Register(Customers customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customers customer)
        {
            _context.Update(customer);
            return Save();
        }

        public bool DeleteCustomer(Customers customer)
        {
            _context.Remove(customer);
            return Save();
        }
    }
}
