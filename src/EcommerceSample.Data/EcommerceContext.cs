using CampaignManagement.Core;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Core;
using ProductManagement.Core;

namespace EcommerceSample.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
