using Microsoft.EntityFrameworkCore;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<WorkEmployee> WorkEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkEmployee>().ToTable("WorkEmployee").HasAlternateKey(k => k.Tin);
        }
    }
}
