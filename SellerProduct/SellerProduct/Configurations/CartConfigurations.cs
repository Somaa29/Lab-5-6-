using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.Description).HasColumnType("nvarchar(100)");
        }
    }
}
