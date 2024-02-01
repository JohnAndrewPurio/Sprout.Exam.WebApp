using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sprout.Exam.WebApp.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            modelBuilder.Entity<WorkEmployee>().HasQueryFilter(x => x.IsDeleted == false).ToTable("WorkEmployee").HasAlternateKey(k => k.Tin);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleEmployeeDeletion();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleEmployeeDeletion();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            HandleEmployeeDeletion();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            HandleEmployeeDeletion();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void HandleEmployeeDeletion()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is WorkEmployee && e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;

                var employee = entity.Entity as WorkEmployee;
                employee.IsDeleted = true;
                employee.DeletedAt = DateTime.UtcNow;

                entity.State = EntityState.Modified;
            }
        }
    }
}
