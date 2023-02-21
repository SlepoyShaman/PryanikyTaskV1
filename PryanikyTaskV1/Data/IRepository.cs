using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Data
{
    public interface IRepository
    {
        public IQueryable<Product> GetAllProducts();
        public Task RemoveProductById(int id);
        public Task AddProduct(Product product);
    }
}
