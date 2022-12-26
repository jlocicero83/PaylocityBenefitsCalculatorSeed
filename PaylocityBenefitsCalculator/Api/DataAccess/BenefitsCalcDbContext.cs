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

            //TODO: From EF Core -  No store type was specified for the decimal property 'Salary' on entity type 'Employee'.
            //This will cause values to be silently truncated if they do not fit in the default precision and scale.
            //Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType',
            //specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
        }
    }
}
