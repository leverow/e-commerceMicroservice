using ECommerce.Api.Dashboard.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Dashboard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product>? Products {get; set;}
}