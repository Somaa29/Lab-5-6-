using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface ICartServices
    {
        public bool CreateCart(Cart p);
        public bool UpdateCart(Cart p);
        public bool DeleteCart(Guid id);
        public List<Cart> GetAllCarts();
        public Cart GetCartById(Guid id);
    }
}
