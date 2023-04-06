using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IBillServices
    {
        public bool CreateBill(Bill p);
        public bool UpdateBill(Bill p);
        public bool DeleteBill(Guid id);
        public List<Bill> GetAllBills();
        public Bill GetBillById(Guid id);
    }
}
