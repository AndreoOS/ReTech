using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);

        builder.Property(e => e.OrderDate)
            .IsRequired();

        builder.Property(e => e.Total)
            .IsRequired();

        builder.Property(e => e.ShippingAddress)
            .HasMaxLength(4095)
            .IsRequired(false);

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.DeliveryDate)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Products)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
