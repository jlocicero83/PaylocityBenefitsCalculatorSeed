using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess
{
    public class BenefitsCalcDbContext : DbContext
    {
        public BenefitsCalcDbContext(DbContextOptions<BenefitsCalcDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(emp => emp.Id);
            modelBuilder.Entity<Dependent>().HasKey(dependent => dependent.Id);
        }
    }
}
