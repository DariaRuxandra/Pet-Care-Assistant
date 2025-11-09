using Microsoft.EntityFrameworkCore;
using Pet_Care_Assistant.Models;

namespace Pet_Care_Assistant.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>().ToTable("Pets");
        }
    }
}