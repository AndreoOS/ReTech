using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => new { x.Id, x.ProductId, x.BrandId });

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.Product)
            .WithMany(e => e.ProductBrands)
            .HasForeignKey(e => e.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Brand)
            .WithMany(e => e.ProductBrands)
            .HasForeignKey(e => e.BrandId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
