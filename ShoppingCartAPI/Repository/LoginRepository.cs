using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;

        public LoginRepository(DataContext context)
        {
            _context = context;
        }
        public Login GetLoginByUsername(string username)
        {
            return _context.Login.Where(l => l.username == username).FirstOrDefault();
        }

        public bool VerifyPassword(string username, string password)
        {
            var login = GetLoginByUsername(username);

            if (login == null || login.password != password)
            {
                return false;
            }

            return true;
        }

        public bool CreateLogin(Login login)
        {
            _context.Add(login);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Login> GetLoginList()
        {
            return _context.Login.OrderBy(x => x.login_id).ToList();
        }
        public bool LoginExists(int login_id)
        {
            return _context.Login.Any(l => l.login_id == login_id);
        }

        public bool UpdateLoginForCustomer(Customers customer)
        {
            /*var login = _context.Login.Where(l => l.customer_id == customer.customer_id).FirstOrDefault();

            var updatedLogin = new Login
            {
                login_id = login.login_id,
                username = customer.username,
                password = customer.password,
                customer_id = customer.customer_id
            };

            _context.Update(updatedLogin);
            return Save();*/

            var login = _context.Login.Where(l => l.customer_id == customer.customer_id).FirstOrDefault();

            if (login != null)
            {
                login.username = customer.username;
                login.password = customer.password;

                _context.Entry(login).State = EntityState.Modified;
            }
            return Save();
        }

        public Login GetLoginByCustomerId(int customer_id)
        {
            return _context.Login.Where(l => l.customer_id == customer_id).FirstOrDefault();
        }

        public bool DeleteLogin(Login login)
        {
            _context.Remove(login);
            return Save();
        }

        public Login GetLogin(int login_id)
        {
            return _context.Login.Where(l => l.login_id == login_id).FirstOrDefault();
        }

        public bool UpdateLogin(Login login)
        {
            _context.Update(login);
            return Save();
        }
    }
}
