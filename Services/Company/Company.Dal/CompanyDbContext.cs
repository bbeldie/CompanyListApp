using Company.Dal.Configurations;
using Company.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Company.Dal
{
    public class CompanyDbContext(DbContextOptions<CompanyDbContext> options) : DbContext(options)
    {
        public virtual DbSet<CompanyModel> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }


    }
}
