using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class CartServices : ICartServices
    {
        ShopDbContext _dbContext;

        public CartServices()
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateCart(Cart p)
        {
            try
            {
                _dbContext.Carts.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCart(Guid id)
        {
            try
            {
                dynamic cart = _dbContext.Carts.Find(id);
                _dbContext.Carts.Remove(cart);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Cart> GetAllCarts()
        {
            return _dbContext.Carts.ToList();
        }

        public Cart GetCartById(Guid id)
        {
            return _dbContext.Carts.FirstOrDefault(p => p.UserId == id);
        }
        public bool UpdateCart(Cart p)
        {
            try
            {

                dynamic cart = _dbContext.Carts.Find(p.UserId);
                cart.Description = p.Description;
                _dbContext.Carts.Update(cart);
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
