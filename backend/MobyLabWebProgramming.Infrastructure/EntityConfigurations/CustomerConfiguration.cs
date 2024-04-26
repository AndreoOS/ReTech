using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);

        builder.Property(e => e.FirstName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasAlternateKey(e => e.Email);

        builder.Property(e => e.Password)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Address)
            .HasMaxLength(4095)
            .IsRequired();

        builder.Property(e => e.DateOfBirth)
            .IsRequired();

        builder.Property(e => e.Gender)
            .IsRequired();

        builder.Property(e => e.Country)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.City)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithOne(e => e.Customer)
            .HasForeignKey<Customer>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Cart)
            .WithOne(e => e.Customer)
            .HasForeignKey<Cart>(e => e.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Orders)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
