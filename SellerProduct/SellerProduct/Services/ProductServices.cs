using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class ProductServices : IProductServices
    {
        ShopDbContext _dbContext;
        public ProductServices() 
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateProduct(Product p)
        {
            try 
            {
                _dbContext.Products.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ) 
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                dynamic product = _dbContext.Products.Find(id);
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch ( Exception ) 
            {
                return false; 
            }
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductByName(string name)
        {
            return _dbContext.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var product = _dbContext.Products.Find(p.Id);
                product.Name = p.Name;
                product.Description = p.Description;
                product.Price = p.Price;
                product.Status = p.Status;
                product.Supplier = p.Supplier;
                product.AvailableQuantity = p.AvailableQuantity;
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
