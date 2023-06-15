using System;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.DAL;

public class EcommerceContext : DbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Product> Products { get; set; }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    } 
}

