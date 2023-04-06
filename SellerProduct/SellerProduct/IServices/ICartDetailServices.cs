using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface ICartDetailServices
    {
        public bool CreateCartDetail(CartDetail p);
        public bool UpdateCartDetail(CartDetail p);
        public bool DeleteCartDetail(Guid id);
        public List<CartDetail> GetAllCartDetails();
        public CartDetail GetCartDetailById(Guid id);
    }
}
