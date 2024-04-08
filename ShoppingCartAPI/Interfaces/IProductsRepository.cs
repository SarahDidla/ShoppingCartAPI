using ShoppingCartAPI.Models;
using System.Diagnostics.Eventing.Reader;

namespace ShoppingCartAPI.Interfaces
{
    public interface IProductsRepository
    {
        ICollection<Products> GetProducts();
        Products GetProduct(int id);
        bool ProductExists(int id);
        bool CreateProduct(Products product);
        bool UpdateProduct(Products product);
        bool DeleteProduct(Products product);
        bool Save();
    }
}
