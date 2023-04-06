using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class RoleServices : IRoleServices
    {
        ShopDbContext _dbContext;
        public RoleServices() 
        {
            _dbContext = new ShopDbContext();
        }
        public bool CreateRole(Role p)
        {
            try
            {
                _dbContext.Roles.Add(p);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRole(Guid id)
        {
            try
            {
                dynamic role = _dbContext.Roles.Find(id);
                _dbContext.Products.Remove(role);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Role> GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return _dbContext.Roles.FirstOrDefault(p => p.RoleId == id);
        }

        public List<Role> GetRoleByName(string name)
        {
            return _dbContext.Roles.Where(p => p.RoleName.Contains(name)).ToList();
        }

        public bool UpdateRole(Role p)
        {
            try
            {
                dynamic role = _dbContext.Roles.Find(p.RoleId);
                _dbContext.Roles.Update(role);
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
