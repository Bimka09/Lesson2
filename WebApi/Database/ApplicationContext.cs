using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Database
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Client> clients { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=k1t2i3f4;Host=localhost;Port=5432;Database=Ebay");
        }
    }
}