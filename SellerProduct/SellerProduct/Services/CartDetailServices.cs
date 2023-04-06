using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class CartDetailServices : ICartDetailServices
    {
        ShopDbContext _dbContext;

        public CartDetailServices()
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateCartDetail(CartDetail p)
        {
            try
            {
                _dbContext.CartDetails.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                dynamic cartdetail = _dbContext.CartDetails.Find(id);
                _dbContext.CartDetails.Remove(cartdetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetail> GetAllCartDetails()
        {
            return _dbContext.CartDetails.ToList();
        }

        public CartDetail GetCartDetailById(Guid id)
        {
            return _dbContext.CartDetails.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateCartDetail(CartDetail p)
        {
            try
            {
                dynamic cartdetail = _dbContext.CartDetails.Find(p.Id);
                cartdetail.Quanity = p.Quantity;
                _dbContext.CartDetails.Update(cartdetail);
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
