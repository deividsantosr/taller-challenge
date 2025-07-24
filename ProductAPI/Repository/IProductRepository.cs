using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
