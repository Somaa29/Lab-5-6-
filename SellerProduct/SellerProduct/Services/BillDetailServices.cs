using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class BillDetailServices : IBillDetailServices
    {
        ShopDbContext _dbContext;
        public BillDetailServices()
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateBillDetails(BillDetails p)
        {
            try
            {
                _dbContext.BillDetails.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBillDetails(Guid id)
        {
            try
            {
                dynamic billdetail = _dbContext.BillDetails.Find(id);
                _dbContext.BillDetails.Remove(billdetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetails> GetAllBillDetails()
        {
            return _dbContext.BillDetails.ToList();
        }

        public BillDetails GetBillDetailsById(Guid id)
        {
            return _dbContext.BillDetails.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateBillDetails(BillDetails p)
        {
            try
            {
                dynamic billdetail = _dbContext.BillDetails.Find(p.Id);
                billdetail.Quanity = p.Quantity;
                billdetail.Price = p.Price;
                _dbContext.BillDetails.Update(billdetail);
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
