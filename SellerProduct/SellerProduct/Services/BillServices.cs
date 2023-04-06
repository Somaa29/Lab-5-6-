using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class BillServices : IBillServices
    {
        ShopDbContext _dbContext;

        public BillServices()
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateBill(Bill p)
        {
            try
            {
                _dbContext.Bills.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBill(Guid id)
        {
            try
            {
                dynamic bill = _dbContext.Bills.Find(id);
                _dbContext.Bills.Remove(bill);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBills()
        {
            return _dbContext.Bills.ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return _dbContext.Bills.FirstOrDefault(p => p.Id == id);
        }
        public bool UpdateBill(Bill p)
        {
            try
            {
                dynamic bill = _dbContext.Bills.Find(p.Id);
                bill.DateTime = DateTime.Now;
                bill.Status = p.Status;
                _dbContext.Users.Update(bill);
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
