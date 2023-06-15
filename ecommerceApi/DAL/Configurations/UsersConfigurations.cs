using System;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerceApi.DAL.Configurations;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Gid);
        builder.Property(x => x.FirstName).HasMaxLength(15);
        builder.Property(x => x.LastName).HasMaxLength(15);
        builder.HasMany(p => p.Products).WithOne(u => u.User);
    }
}


