using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess
{
    public class BenefitsCalcDbContext : DbContext
    {
        public BenefitsCalcDbContext(DbContextOptions<BenefitsCalcDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Dependent> Dependent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
        }
    }
}
