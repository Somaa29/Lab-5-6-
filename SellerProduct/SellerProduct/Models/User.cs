namespace SellerProduct.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Bill> Bills { get; set;}
        public virtual Role Role{ get; set; }
        public virtual Cart Cart { get; set; }


    }
}
