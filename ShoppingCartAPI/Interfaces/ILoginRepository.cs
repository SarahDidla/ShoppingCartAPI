using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Interfaces
{
    public interface ILoginRepository
    {
        ICollection<Login> GetLoginList();
        Login GetLogin(int login_id);
        Login GetLoginByUsername(string username);
        bool VerifyPassword(string username, string password);
        bool CreateLogin(Login login);
        bool Save();
        /*bool UpdateLogin(Login login);*/
        bool LoginExists(int login_id);
        bool UpdateLoginForCustomer(Customers customer);
        Login GetLoginByCustomerId(int customer_id);
        bool DeleteLogin(Login login);
        bool UpdateLogin(Login login);
        
    }
}
