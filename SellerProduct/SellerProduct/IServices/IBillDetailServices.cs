using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IBillDetailServices
    {
        public bool CreateBillDetails(BillDetails p);
        public bool UpdateBillDetails(BillDetails p);
        public bool DeleteBillDetails(Guid id);
        public List<BillDetails> GetAllBillDetails();
        public BillDetails GetBillDetailsById(Guid id);
    }
}
