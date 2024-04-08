using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _context;

        public ProductsRepository(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(x => x.product_id == id);
        }

        public bool CreateProduct(Products product)
        {
            _context.Add(product);
            return Save();
        }

        public ICollection<Products> GetProducts()
        {
            return _context.Products.OrderBy(x => x.product_id).ToList();    
        }

        public Products GetProduct(int id)
        {
            return _context.Products.Where(x => x.product_id == id).FirstOrDefault();
        }

        public bool UpdateProduct(Products product)
        {
            _context.Update(product);
            return Save();
        }

        public bool DeleteProduct(Products product)
        {
            _context.Remove(product);
            return Save();
        }
    }
}
