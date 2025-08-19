using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public  class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add your model configuration logic here
            base.OnModelCreating(modelBuilder);
        }
    }
}
