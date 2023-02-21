using Microsoft.EntityFrameworkCore;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Data
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAllProducts()
            => _context.Products.Select(p => p);

        public async Task RemoveProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new Exception($"Product with id = {id} does not exist");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
