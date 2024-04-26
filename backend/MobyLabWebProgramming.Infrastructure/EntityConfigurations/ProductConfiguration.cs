using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(4095)
            .IsRequired(false);

        builder.Property(e => e.Price)
            .IsRequired();

        builder.Property(e => e.Color)
            .HasMaxLength(63)
            .IsRequired();

        builder.Property(e => e.Size)
            .HasMaxLength(63)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Cart)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Order)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.ProductBrands)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
