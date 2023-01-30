using ECommerce.Api.Merchant.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Merchant.Context;

public class AppDbContext : DbContext
{
    public DbSet<Organization>? Organizations { get; set; }
    public DbSet<Users>? User { get; set; }
    public DbSet<Product>? Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}