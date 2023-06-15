using System;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerceApi.DAL.Configurations;

public class ProductsConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Gid);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(p => p.Title).HasMaxLength(30);
        builder.Property(p => p.Photo).HasMaxLength(300);
        builder.Property(p => p.Price);
        builder.Property(p => p.Quantity);
        builder.Property(p => p.Stock);
        builder.Property(p => p.Delivery);
    }
}

