using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasColumnType("varchar(256)");
            builder.Property(x => x.PassWord).HasColumnType("varchar(256)");
            builder.HasOne(p => p.Role).WithMany(p=>p.Users).HasForeignKey(p =>p.RoleId);
        }
    }
}
